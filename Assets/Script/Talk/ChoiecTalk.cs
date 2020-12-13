using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiecTalk : MonoBehaviour
{ 
    public GameObject choiceObject;
    public GameObject[] choicePanel;
    public Text[] choiceText;
    public GameObject[] choiceCursor;
    public Gamemanager gamemanager;
    public MoveOther moveOther;
    public Playermanager playermanager;
    public GameObject colliderObject;
    private Collider2D objectCollider;

    public bool keyInput;
    private int count; // 배열의 크기
    private int result; // 선택한 선택창.
    private int Action;

    private void Awake()
    {
        objectCollider = colliderObject.GetComponent<Collider2D>();
    }

    public void Question() // 질문창 띄움
    {
        result = 0;
        count = choicePanel.Length - 1;
        choiceObject.SetActive(true);
        choiceCursor[0].SetActive(true);
        keyInput = true;
    }

    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                keyInput = false;
                StartCoroutine(QuestionTalk(result, new int[] { 300, 400 }));
                ExitChoice();
            }
        }
    }

    public void Selection()
    {
        for(int i = 0; i < choicePanel.Length; i++)
        {
            choiceCursor[i].SetActive(false);
        }
        choiceCursor[result].SetActive(true);
    }

    void ExitChoice()
    {
        count = -1;
        choiceObject.SetActive(false);
        choiceCursor[0].SetActive(false);
        choiceCursor[1].SetActive(false);
        keyInput = false;
    }

    IEnumerator QuestionTalk(int result, int[] cutNumber)
    {
        gamemanager.talkindex = 0;
        for(int i = 0; i < result + 1; i++)
        {
            if (result == i)
                gamemanager.CutSceneTalk(cutNumber[i]);
            Action = cutNumber[i];
        }
        yield return new WaitUntil(() => !playermanager.isaction);
        //Debug.Log(playermanager.isaction);
        if (Action == 300)
        {
            moveOther.PlayerMove();
            OffCollider();
        }
    }

    void OffCollider()
    {
        objectCollider.enabled = false;
    }
}