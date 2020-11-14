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

    public GameObject talkwindow; // 대화창

    public int CutNumber;
    public int talkindex; // 대사 진행도
    public int QusetId;

    public void GetObjectData(GameObject TalkObjectData)
    {
        TalkObject = TalkObjectData;
        Objectdata obdata = TalkObject.GetComponent<Objectdata>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
        Talk(obdata.id, obdata.isnpc);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void CutSceneTalk(int id)
    {
        CutNumber = id;
        Talk(id, false);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void Talk(int id, bool isnpc)
    {
        if (cameramanager.isCameraMove)
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

        if (isnpc)
        {
            typeEffect.EffectStart(talkdata);
        }
        else
            typeEffect.EffectStart(talkdata);  // 대화창의 Text에 GetText의 return을 넣음 

        talkindex++;
        playermanager.isaction = true;
    }

    void TalkEnd() // 변수 초기화 및 함수 종료
    {
        talkindex = 0;
        playermanager.isaction = false;
        QusetId = 10;
        CutNumber = 0;
    }
}
