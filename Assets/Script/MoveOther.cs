using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOther : MonoBehaviour
{
    public GameObject MoveObject;
    Vector2 NpcVec;
    public Queue<string> DirSave;
    bool NotCoroutine;
    bool NPCdontmove = true;
    int count;
    public int walkcount;
    float speed = 0.1f;

    public string[] directions;
    public int frequency;

    private Animator animator;

    private void Awake()
    {
        animator = MoveObject.GetComponent<Animator>();
        DirSave = new Queue<string>();
    }

    public void PlayerMove()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            Move(directions[i]);
        }
    }

    public void Move(string dir)
    {
        DirSave.Enqueue(dir); // DirSave dir을 넣음
        if (!NotCoroutine) //지정한 방향값이 다수일 경우 코루틴이 다중반복실행되어 오브젝트가 개빨리 움직이는 에러가 나서 코루틴 시작 조건을 만듬
        {
            StartCoroutine(NpcOtherMove());
            NotCoroutine = true;
        }
    }

    IEnumerator NpcOtherMove()
    {
        while (DirSave.Count != 0) // DirSave의 값이 모두 제거되면 멈춤
        {
            string direction = DirSave.Dequeue(); //queue에서 제거되는 값이 switch문의 값이됨
            NpcVec.Set(0, 0); //코루틴을 한번 돌고나면 백터값을 초기화(x,y가 동시에 1을가지면 안되기 때문)

            switch (direction) //switch 문에는 string값이 와도 상관이 없음
            {
                case ("UP"):
                    NpcVec.y = 1f;
                    break;
                case ("DOWN"):
                    NpcVec.y = -1f;
                    break;
                case ("RIGHT"):
                    NpcVec.x = 1f;
                    break;
                case ("LEFT"):
                    NpcVec.x = -1f;
                    break;
                case ("NONE"):
                    NpcVec.x = 0;
                    NpcVec.y = 0;
                    break;
            }
            OtherAinmation(); // 이동 방향에 따른 애니메이션 

            while (count < walkcount)
            {
                transform.Translate(NpcVec.x * speed, NpcVec.y * speed, 0);
                //코루틴에서 백터값을 초기화하기 때문에 x,y를 동시에 움직여도 대각선 이동은 일어나지 않음
                count++;
                yield return new WaitForSeconds(0.01f);
            }
            
            count = 0;
            if (NPCdontmove)
                yield return new WaitForSeconds(0.3f);
        }
        NotCoroutine = false; //while문이 끝난후 다시 false로 바꿔 코루틴이 돌아가게함
        animator.SetBool("Walking", false); // 애니메이션 끝후 방향전환

        OtherLook(0, -1);
    }

    IEnumerator NpcMoveCoroutine()
    {
        if (directions.Length != 0) //지정한 방향값이 있는지 확인
        {
            for (int i = 0; i < directions.Length; i++) //지정한 방향값의 크기만큼 움직임
            {
                switch (frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                yield return new WaitUntil(() => DirSave.Count < 2);
                Move(directions[i]);

                //NPC가 무한반복하여 움직이게 하는 code
                if (i == directions.Length - 1)
                    i = -1; // 반복문이 배열의 크기만큼 다 돌았으면 i에 -1을 대입해 다시0부터 시작하게함
            }
        }
    }

    void OtherAinmation()
    {
        animator.SetBool("Walking", true);
        animator.SetFloat("DirX", NpcVec.x);
        animator.SetFloat("DirY", NpcVec.y);
    }

    void OtherLook(int dirX, int dirY)
    {
        animator.SetFloat("DirX", dirX);
        animator.SetFloat("DirY", dirY);
    }
}