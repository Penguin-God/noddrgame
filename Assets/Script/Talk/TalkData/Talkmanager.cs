using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Talkmanager : MonoBehaviour
{
    public Dictionary<int, string[]> talkdata;

    private void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        TalkData();
    }

    void TalkData() //대화 내용
    {
        talkdata.Add(100, new string[] { "어 보이시나요?", ".............  저기요?", "아 화면을 꺼놨네" });
        talkdata.Add(200, new string[] { "안녕하세요? 그......... 혹시 우리 초면인가요?" });
        talkdata.Add(300, new string[] { "그래요? 그럼 우리 암호가 뭐죠?" });
        talkdata.Add(400, new string[] { "처음인가요? 반가워요 저는 '' 라고 해요 잘부탁드려요", "암호는 '보자기'에요 그럼 나중에 봐요" });
        talkdata.Add(500, new string[] { "아쉽게 틀렸네요 다음에는 맞춰주세요^^" });
        talkdata.Add(600, new string[] { "" });
        talkdata.Add(700, new string[] { "" });
        talkdata.Add(800, new string[] { "" });
        talkdata.Add(900, new string[] { "" });
        talkdata.Add(1000, new string[] { "" });
    }

    public string GetTalkData(int id, int talkindex) // 대화 진행도에 따라 대사, null을 리턴함
    {
        if (talkindex == talkdata[id].Length) // 모든 대사를 보여주면 null을 리턴함 
            return null;
        else
            return talkdata[id][talkindex]; // 대화가 안끝났으면 다음 대사를 리턴함
    }
}
