using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public Cameramanager cameramanager;
    public Talkmanager talkmanager;
    public Playermanager playermanager;
    public TypingEffect typingEffect;
    public ChoiecTalk choiecTalk;

    public GameObject talkwindow; // 대화창

    public int CutNumber;
    public int talkindex; // 대사 진행도

    private Objectdata obdata;

    public void FiledTalk(GameObject TalkObjectData)
    {
        GetObjectData(TalkObjectData);
        Talk(obdata.id);
    }

    public Objectdata GetObjectData(GameObject TalkObjectData)
    {
        GameObject TalkObject = TalkObjectData;
        obdata = TalkObject.GetComponent<Objectdata>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
        return obdata;
    }

    public void CutSceneTalk(int id)
    {
        CutNumber = id;
        Talk(id);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void Talk(int id)
    {
        if (cameramanager.isCameraMove || choiecTalk.keyInput) return;
        if (typingEffect.isTyping) // 타이핑 애니메이션중에 대화 넘기기를 시도할 때
        {
            typingEffect.FillText(); 
            return;
        }
        
        string talkdata = talkmanager.GetTalkData(id, talkindex);  // get talkText

        if (talkdata == null) TalkEnd(); // 대화 끝날 시
        else TalkType(talkdata); // 대사 출력
    }

    void TalkEnd() // 변수 초기화 및 함수 종료
    {
        talkindex = 0;
        CutNumber = 0;
        ShowTalkwinow(false);
    }

    void TalkType(string talkdata) // 대화하는 대상에 따라 다른 방식으로 talk함
    {
        typingEffect.EffectStart(talkdata);  // 대화창의 Text에 GetText의 return을 넣음 
        talkindex++;
        ShowTalkwinow(true);
    }

    void ShowTalkwinow(bool show) // 대화창 뛰우는 함수
    {
        playermanager.isaction = show;
        talkwindow.SetActive(show);
    }

    public IEnumerator QuestionCoroutine(int id, int[] questionId) // 대화창에 질문을 띄우는 함수
    {
        Talk(id);
        yield return new WaitUntil(() => talkmanager.talkdata[id].Length == talkindex && !typingEffect.isTyping);
        typingEffect.EndCursor.SetActive(false);
        choiecTalk.Question(questionId);
    }

    public QuestionObjectData GetQuestionData(GameObject TalkObjectData)
    {
        GameObject TalkObject = TalkObjectData;
        QuestionObjectData questionData = TalkObject.GetComponent<QuestionObjectData>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
        StartCoroutine(QuestionCoroutine(questionData.talkId, questionData.questionId));
        return questionData;
    }

    bool IsEndTalk()
    {
        return true;
    }
}