using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Talkmanager : MonoBehaviour
{
    public Dictionary<int, string[]> talkdata;

    private void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        TalkData();
    }

    void TalkData()
    {
        //대화 내용
        talkdata.Add(100, new string[] { "공부할 때 쓰는 책상이다.", "여기에 앉은지는 꽤 오래 된 것 갔다." });
        talkdata.Add(200, new string[] { "시간이 늦었군", "잠이나 잘까?"});
        talkdata.Add(300, new string[] { "그래 잠이나 자자" });
        talkdata.Add(400, new string[] { "아니 아직 안잘래", "근데 왜?", "지금 안자면 뭐하게? 또 누워서 핸드폰이나 쳐 하면서  새벽 5시에 자서 3시에 일어나게?", "에휴 시벌" });
        talkdata.Add(500, new string[] { "ggg" });
        talkdata.Add(600, new string[] { "이것은 컷씬대화 실험으로", "대사가 끊기지않으면서", "대사가 잘 나오면 성공입니다." });
        talkdata.Add(700, new string[] { "헉... 헉......", "읏.. 으읏"});
        talkdata.Add(800, new string[] { "...........", "현타오네", "다 했으니 잠이나 자야겠다."});
    }

    public string GetTalkData(int id, int talkindex) // 대화 진행도에 따라 대사, null을 리턴함
    {
        if (talkindex == talkdata[id].Length) // 모든 대사를 보여주면 null을 리턴함 
            return null;
        else
            return talkdata[id][talkindex]; // 대화가 안끝났으면 다음 대사를 리턴함
    }
}
