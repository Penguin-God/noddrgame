using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gamemanager : MonoBehaviour
{
    static public Gamemanager instance;
    public GameObject crash;
    public GameObject scan;
    public GameObject talkwindow;
    public Talkmanager talkmanager;
    public int talkindex;
    public bool isaction;
    public Text 대화;

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

    public void Action(GameObject scanob)
    {
        scan = scanob;
        Objectdata obdata = scan.GetComponent<Objectdata>();
        Talk(obdata.id, obdata.isnpc);
        talkwindow.SetActive(isaction);
    }

    public void 충돌시대화(GameObject crashob)
    {
        crash = crashob;
        Objectdata obdata = crash.GetComponent<Objectdata>();
        Talk(obdata.id, obdata.isnpc);
        talkwindow.SetActive(isaction);
    }

    void Talk(int id, bool isnpc)
    {
        string talkdata = talkmanager.GetTalk(id, talkindex);
        if (talkdata == null)
        {
            //이야기 끝
            talkindex = 0;
            isaction = false;
            return;
        }

        if (isnpc)
        {
            대화.text = talkdata;
        }
        else
        {
            대화.text = talkdata;
        }
        talkindex++;
        isaction = true;
    }
}
