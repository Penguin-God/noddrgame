using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOther : MonoBehaviour
{
    Vector2 NpcVec;
    public Queue<string> DirSave;
    bool NotCoroutine;
    bool NPCdontmove;
    public int count;
    public int walkcount;
    int speed = 5;

    private void Awake()
    {
        DirSave = new Queue<string>();
        Move("UP");
    }

    private void Update()
    {
        
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

            while (true)
            {
                if (NPCdontmove)
                    yield return new WaitForSeconds(1f);
                else
                    break;
            }

            while (count < walkcount)
            {
                transform.Translate(NpcVec.x * speed, NpcVec.y * speed, 0);
                Debug.Log("Asd");
                //코루틴에서 백터값을 초기화하기 때문에 x,y를 동시에 움직여도 대각선 이동은 일어나지 않음
                count++;
                yield return new WaitForSeconds(0.01f);
            }
            count = 0;
        }
        NotCoroutine = false; //while문이 끝난후 다시 false로 바꿔 코루틴이 돌아가게함
    }
}
