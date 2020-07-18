using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gamemanager : MonoBehaviour
{
    //static public Gamemanager instance;
    public GameObject TalkObject;
    public GameObject talkwindow;
    public Text 대화창텍스트;
    public Talkmanager talkmanager;
    public Npcmanager npcmanager;

    public int talkindex;
    public int QusetId;

    public bool isaction;
    public bool 컷씬;

    //씬 이동시 이 코드를 가진 스크립트를 가진 오브텍트는 파괴되지 않음
    //private void Start()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    public void 대화오브젝트정보보내기(GameObject TalkObjectData)
    {
        TalkObject = TalkObjectData;
        Objectdata obdata = TalkObject.GetComponent<Objectdata>();
        Talk(obdata.id, obdata.isnpc);
        talkwindow.SetActive(isaction);
    }

    public void 컷씬대화()
    {
        if (컷씬)
        {
            Talk(500, false);
            talkwindow.SetActive(isaction);
        }
    }

    void Talk(int id, bool isnpc)
    {
        string talkdata = talkmanager.대화창띄우기(id, talkindex);
        if (talkdata == null)
        {
            //이야기 끝났을 시 대화창끄고 talkindex초기화 
            talkindex = 0;
            isaction = false;
            QusetId = 10;
            컷씬 = false;
            //대화가 끝날 때 talkindex와 isaction이 변동이 없어야 하므로 return으로 함수 강제종료 
            return;
        }

        if (isnpc)
        {
            대화창텍스트.text = talkdata;
        }
        else
        {
            //대화창의 text를 Talkmanager에 있는 대화내용으로 바꿈
            대화창텍스트.text = talkdata;
        }
        talkindex++;
        isaction = true;
    }
}
