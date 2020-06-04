using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkmanager : MonoBehaviour
{
    Dictionary<int, string[]> talkdata;

    private void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        TalkData();
    }

    void TalkData()
    {
        //대화 내용
        talkdata.Add(100, new string[] { "공부할 때 쓰는 책상이다.", "여기에 앉은지는 꽤 오래 된 것 갔다." });
        talkdata.Add(400, new string[] { "나는 히토미를 보고 딸친다고 아빠한테 집에서 쫒껴났다.",
                                         "일주일동안 금딸을 하고 돌아오란다.",
                                        "뭔 개 좆같은"});
        talkdata.Add(300, new string[] { "지금은 집에 들어갈 수 없다." });
    }

    public string GetTalk(int id, int talkindex)
    {
        //모든 대화를 보여주면 끝
        if (talkindex == talkdata[id].Length)
            return null;
        //대화가 안끝났으면 다음 대화를 보여줌
        else
            return talkdata[id][talkindex];
    }
}
