using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiecTalk : MonoBehaviour
{ 
    public GameObject choiceObject;
    public GameObject[] choicePanel;
    //public Text[] choiceText;
    public GameObject[] choiceCursor;
    public Gamemanager gamemanager;
    public MoveOther moveOther;
    public Playermanager playermanager;
    private Animator animator;

    public GameObject Player;
    public GameObject colliderObject;
    private Collider2D objectCollider;
    private Vector2 BedVec;

    public bool keyInput;
    private int count; // 배열의 크기
    private int result; // 선택한 선택창.
    private int Action;

    private void Awake()
    {
        objectCollider = colliderObject.GetComponent<Collider2D>();
        animator = playermanager.GetComponent<Animator>();
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
        if (keyInput) // 질문창이 뛰어지면 버튼 입력을 받음에 따라 다른 코드가 실행됨
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

    public void Selection() // 현재 선택되어있는 커서 빼고 다 지우는 함수
    {
        for(int i = 0; i < choicePanel.Length; i++)
        {
            choiceCursor[i].SetActive(false);
        }
        choiceCursor[result].SetActive(true);
    }

    void ExitChoice() // 질문 끝난 후 오브젝트 및 변수 정리
    {
        count = -1;
        choiceObject.SetActive(false);
        choiceCursor[0].SetActive(false);
        choiceCursor[1].SetActive(false);
        keyInput = false;
    }

    IEnumerator QuestionTalk(int result, int[] cutNumber) // 질문 선택에 따른 액션
    {
        gamemanager.talkindex = 0;
        for(int i = 0; i < result + 1; i++)
        {
            if (result == i)
                gamemanager.CutSceneTalk(cutNumber[i]);
            Action = cutNumber[i];
        }
        yield return new WaitUntil(() => !playermanager.isaction);
        if (Action == 300)
        {
            StartCoroutine(Sleep(5));
        }
    }

    void OffCollider() 
    {
        objectCollider.enabled = false;
    }

    IEnumerator Sleep(int walkcount) // 잠자는 코루틴
    {
        OffCollider();
        int count = walkcount;
        BedVec = new Vector3(Player.transform.position.x, colliderObject.transform.position.y - Player.transform.position.y, Player.transform.position.x);
        yield return new WaitForSeconds(0.5f);
        Y_Move_Animation(BedVec);
        while (count > 0)
        {
            Player.transform.Translate(0, BedVec.y / walkcount, 0);
            yield return new WaitForSeconds(0.01f);
            count--;
        }
        moveOther.PlayerMove();
    }

    void Y_Move_Animation(Vector3 DirY)
    {
        animator.SetBool("Walking", true);
        animator.SetFloat("DirX", 0);
        if (DirY.y > 0)
            animator.SetFloat("DirY", 1);
        else
            animator.SetFloat("DirY", -1);
    }
}