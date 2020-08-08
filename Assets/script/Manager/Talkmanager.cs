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
        talkdata.Add(200, new string[] { "12355555"});
        talkdata.Add(300, new string[] { "지금은 집에 들어갈 수 없다." });
        talkdata.Add(400, new string[] { "아니씨발 실험하는데.",
                                         "대사를 같게 쳐 적으면.",
                                         "그게 실험이냐 씨빨?"});
        talkdata.Add(500, new string[] { "헉.. 헉....." });
    }

    public string 대화창띄우기(int id, int talkindex)
    {
        //모든 대화를 보여주면 끝
        if (talkindex == talkdata[id].Length)
            return null;
        //대화가 안끝났으면 다음 대화를 보여줌
        else
            return talkdata[id][talkindex];
    }
}
