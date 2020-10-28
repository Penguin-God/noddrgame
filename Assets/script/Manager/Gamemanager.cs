using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public GameObject TalkObject;
    public Talkmanager talkmanager;
    public Playermanager playermanager;
    public TypeEffect typeEffect;

    public GameObject talkwindow; // 대화창

    public int CutNumber;
    public int talkindex; // 대사 진행도
    public int QusetId;

    public void 오브젝트정보확인(GameObject TalkObjectData)
    {
        TalkObject = TalkObjectData;
        Objectdata obdata = TalkObject.GetComponent<Objectdata>(); // obdata에 오브젝트에 있는 Objectdata Script를 담음
        Talk(obdata.id, obdata.isnpc);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void 컷씬대화(int id)
    {
        Talk(id, false, true);
        talkwindow.SetActive(playermanager.isaction);
    }

    public void Talk(int id, bool isnpc, bool isCut = false)
    {
        if (typeEffect.isTyping) // text창에 대사 채울때는 변수 값 변경 없게하기 위해 return
        {
            typeEffect.GetText(""); // 어차피 text창 채우는 거라서 의미없음
            return;
        }

        string talkdata = talkmanager.GetText(id, talkindex); 

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
            typeEffect.GetText(talkdata);
        }
        else
            typeEffect.GetText(talkdata);  // 대화창의 Text에 GetText의 return을 넣음 

        talkindex++;
        playermanager.isaction = true;
        if (isCut) CutNumber = id;
    }
}
