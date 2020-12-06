using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //커스텀class가 인스펙터 창에 나오게 하기위한 명령어
public class NPCMove
{
    public string[] direction; // npc가 움직일 방향 설정

    [Range(1, 5)]
    [Tooltip("1 = 천천히, 2 = 조금천천히, 3 = 보통, 4 = 빠르게, 5 = 연속적으로")] // Tooltip : 인스펙터 창에 마우스오버 시 설명이 나옴
    //[Range(1, 5)] : frequency가 인스펙터 창에 1~5까지 조절할 수 있는 스크롤바가 나옴
    public int frequency; //npc가 얼마나 지정된 방향으로 빈번하게 움직일 것인가
}

public class Npcmanager : 변수저장소
{
    [SerializeField] //커스텀 class가 인스펙터 창에 나오게 하기위한 명령어
    public NPCMove npc;//커스텀class를 변수로 초기화(스크립트 초기화와 사용방법이 같음)

    public LayerMask layermask;
    public RaycastHit2D raycasthit;

    Vector2 NpcVec;

    public bool NPCdontmove;
    private bool NotCortoutine;

    public int walkcount;
    protected int count;

    //Queue : 선입선출 자료구조(먼저넣은값이 가장 앞에있고 값을 뺄 때 가장 앞에 있는값을 먼저 빼는 자료구조)
    //Queue자료구조에 a,b,c를 순서대로 넣으면 a,b,c 순서대로 값이 들어가고 값을 뻬려고 하면 a,b,c순서대로 값이 나온다.
    //Enqueue() : Queue의 끝 부분에 값을 넣는 것, Dequeue : Queue 의 시작 부분에서 개체를 제거하고 반환함. 
    public Queue<string> NpcDirSave;

    private void Awake()
    {
        NpcDirSave = new Queue<string>();
    }

    public void Move(string dir)
    {
        NpcDirSave.Enqueue(dir); // NpcDirSave dir을 넣음
        if (!NotCortoutine) //지정한 방향값이 다수일 경우 코루틴이 다중반복실행되어 오브젝트가 개빨리 움직이는 에러가 나서 코루틴 시작 조건을 만듬
        {
            StartCoroutine(NpcOtherMove());
            NotCortoutine = true;
        }
    }

    IEnumerator NpcOtherMove()
    {
        while (NpcDirSave.Count != 0) // NpcDirSave의 값이 모두 제거되면 멈춤
        {
            string direction = NpcDirSave.Dequeue(); //queue에서 제거되는 값이 switch문의 값이됨
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
                //코루틴에서 백터값을 초기화하기 때문에 x,y를 동시에 움직여도 대각선 이동은 일어나지 않음
                count++;
                yield return new WaitForSeconds(0.01f);
            }
            count = 0;
        }
        NotCortoutine = false; //while문이 끝난후 다시 false로 바꿔 코루틴이 돌아가게함
    }

    IEnumerator NpcMoveCoroutine()
    {
        if (npc.direction.Length != 0) //지정한 방향값이 있는지 확인
        {
            for (int i = 0; i < npc.direction.Length; i++) //지정한 방향값의 크기만큼 움직임
            {
                switch (npc.frequency)
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

                yield return new WaitUntil(() => NpcDirSave.Count < 2);
                Move(npc.direction[i]);

                //NPC가 무한반복하여 움직이게 하는 code
                if (i == npc.direction.Length - 1)
                    i = -1; // 반복문이 배열의 크기만큼 다 돌았으면 i에 -1을 대입해 다시0부터 시작하게함
            }
        }
    }

    private void FixedUpdate() //Ray에 콜라이더가 걸리면 정지
    {
        Vector2 start;
        Vector2 end;
        start = transform.position;
        end = start + new Vector2(NpcVec.x * speed * walkcount , NpcVec.y * speed * walkcount);

        RaycastHit2D rayhit = Physics2D.Linecast(start, end, layermask);
        if (rayhit.transform != null)
            NPCdontmove = true;
    }
}
