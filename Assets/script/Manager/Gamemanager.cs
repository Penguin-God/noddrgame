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
        Talk(id, false, true);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void Talk(int id, bool isnpc, bool isCut = false)
    {
        if (cameramanager.isCameraMove)
            return;
        if (typeEffect.isTyping) // 타이핑 애니메이션중에 대화 넘기기를 시도할 때
        {
            typeEffect.FillText(); 
            return;
        }

        string talkdata = talkmanager.GetTalkData(id, talkindex); 

        if (talkdata == null) // 대화창 뛰우기에서 null을 리턴받으면 관련 변수를 초기화시킴 return으로 함수 강제종료
        {   
            talkindex = 0;
            playermanager.isaction = false;
            QusetId = 10;
            CutNumber = 0;
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
        if (isCut) CutNumber = id;
    }
}
