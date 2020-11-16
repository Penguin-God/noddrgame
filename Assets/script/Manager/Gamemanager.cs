using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public GameObject TalkObject;
    public Cameramanager cameramanager;
    public Talkmanager talkmanager;
    public Playermanager playermanager;
    public TypingEffect typingEffect;
    public ChoiecTalk choiecTalk;

    public GameObject talkwindow; // 대화창

    public int CutNumber;
    public int talkindex; // 대사 진행도
    public int QusetId;

    private Objectdata obdata;

    public void FiledTalk(GameObject TalkObjectData)
    {
        GetObjectData(TalkObjectData);
        TalkAction();
    }

    void GetObjectData(GameObject TalkObjectData)
    {
        TalkObject = TalkObjectData;
        obdata = TalkObject.GetComponent<Objectdata>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
    }

    void TalkAction()
    {
        Talk(obdata.id, obdata.isnpc, obdata.isQuestion);
        talkwindow.SetActive(playermanager.isaction);
    } 

    public void CutSceneTalk(int id)
    {
        CutNumber = id;
        Talk(id, false, false);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void QuestionTalk(string talkdata)
    {
        typingEffect.EffectStart(talkdata);
        StartCoroutine(QuestionCoroutine());
    }


    public void Talk(int id, bool isnpc, bool isQuestion)
    {
        if (cameramanager.isCameraMove || choiecTalk.keyInput) 
            return;
        if (typingEffect.isTyping) // 타이핑 애니메이션중에 대화 넘기기를 시도할 때
        {
            typingEffect.FillText(); 
            return;
        }
        // 대화시작
        string talkdata = talkmanager.GetTalkData(id, talkindex);  // get talkText

        if (talkdata == null) // 대사 다 출력 시
        {
            TalkEnd();
        }
        else if (talkmanager.talkdata[id].Length == talkindex + 1 && isQuestion)
            QuestionTalk(talkdata);
        else
            TalkType(isnpc, talkdata);
    }

    void TalkEnd() // 변수 초기화 및 함수 종료
    {
        talkindex = 0;
        playermanager.isaction = false;
        QusetId = 10;
        CutNumber = 0;
    }

    void TalkType(bool isnpc, string talkdata) // 대화하는 대상에 따라 다른 방식으로 talk함
    {
        //Debug.Log("a");
        if (isnpc)
        {
            typingEffect.EffectStart(talkdata);
        }
        else
            typingEffect.EffectStart(talkdata);  // 대화창의 Text에 GetText의 return을 넣음 

        talkindex++;
        playermanager.isaction = true;
    }

    IEnumerator QuestionCoroutine()
    {
        yield return new WaitUntil(() => !typingEffect.isTyping);
        typingEffect.EndCursor.SetActive(false);
        choiecTalk.Question();
    }
}
