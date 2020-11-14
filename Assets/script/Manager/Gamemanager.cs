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
    public TypeEffect typeEffect;
    public ChoiecUI choiecUI;

    public GameObject talkwindow; // 대화창

    public int CutNumber;
    public int talkindex; // 대사 진행도
    public int QusetId;

    public void GetObjectData(GameObject TalkObjectData)
    {
        TalkObject = TalkObjectData;
        Objectdata obdata = TalkObject.GetComponent<Objectdata>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
        Talk(obdata.id, obdata.isnpc, obdata.isQuestion);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void CutSceneTalk(int id)
    {
        CutNumber = id;
        Talk(id, false, false);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void Talk(int id, bool isnpc, bool isQuestion)
    {
        if (cameramanager.isCameraMove || choiecUI.keyInput)
            return;
        if (typeEffect.isTyping) // 타이핑 애니메이션중에 대화 넘기기를 시도할 때
        {
            typeEffect.FillText(); 
            return;
        }

        string talkdata = talkmanager.GetTalkData(id, talkindex);  // get talkText
        if (talkdata == null) // 대사 다 출력 시
        {
            TalkEnd();
            return;
        }

        TalkType(isnpc, isQuestion, talkdata);
    }

    void TalkEnd() // 변수 초기화 및 함수 종료
    {
        talkindex = 0;
        playermanager.isaction = false;
        QusetId = 10;
        CutNumber = 0;
    }

    void TalkType(bool isnpc, bool isQuestion, string talkdata)
    {
        if (isnpc)
        {
            typeEffect.EffectStart(talkdata);
        }
        else if (isQuestion)
        {
            typeEffect.EffectStart(talkdata);
            StartCoroutine(QuestionCoroutine());
        }
        else
            typeEffect.EffectStart(talkdata);  // 대화창의 Text에 GetText의 return을 넣음 

        talkindex++;
        playermanager.isaction = true;
    }

    IEnumerator QuestionCoroutine()
    {
        yield return new WaitUntil(() => !typeEffect.isTyping);
        typeEffect.EndCursor.SetActive(false);
        choiecUI.Question();
    }
}
