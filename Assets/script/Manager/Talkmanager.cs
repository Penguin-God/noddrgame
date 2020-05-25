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
        talkdata.Add(100, new string[] { "내가자는 곳이다." });
        talkdata.Add(200, new string[] { "옷장이다.", "여러옷이 있지만 보기만 해도 역겨운 교복이 유독 눈에 들어온다." });
        talkdata.Add(300, new string[] { "공부할 때 쓰는 책상이다.", "여기에 앉은지는 꽤 오래 된 것 갔다." });
        talkdata.Add(400, new string[] { "20XX년 X월 X일 금요일", "나는 중학교 1학년 때부터 고2 현재까지 단 하루도 거르지 않고 딸딸이를 쳐왔다.",
                                         "하지만 언제서부턴가 ddr은 쾌감보다는 자괴감과 현자타임만 주게 되었고,결국 나는 지난달부터 금딸을 시작하였다.",
                                         "몇번의 실패가 있었지만 현재는 금딸 12일차 최종 목표인 금딸2주까지2일밖에 남지 않았다.",
                                         "하지만 남은 2일이 모두 주말이고 심지어 부모님까지 출장으로 집에 없다.",
                                         "이 일기는 나의 지난 12일간의 노력을 순간의 욕망으로 헛되지 않기위해 쓰는 것이다 부디 내가이 혼란한 주말을 견디고 평안히 금딸하길 ", "-현재 금딸 13일차-",
                                         "가슴이 웅장해진다."});
        talkdata.Add(500, new string[] { "지금은 집에 들어갈 수 없다." });
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
