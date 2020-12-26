using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmanager : MonoBehaviour
{
    public int questionId;

    Dictionary<int, QuestionData> questionList;

    private void Awake()
    {
        questionList = new Dictionary<int, QuestionData>();
        AddData();
    }

    void AddData()
    {
        questionList.Add(10, new QuestionData("잠 여부", 200));
    }
}
