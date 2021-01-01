using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmanager : MonoBehaviour
{
    public int questionId;

    Dictionary<int, QuestionObjectData> questionList;

    private void Awake()
    {
        questionList = new Dictionary<int, QuestionObjectData>();
        //AddData();
    }

    //void AddData()
    //{
    //    questionList.Add(10, new QuestionObjectData("잠 여부", 200));
    //}
}
