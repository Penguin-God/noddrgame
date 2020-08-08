using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커스텀class가 인스펙터 창에 나오게 하기위한 명령어
[System.Serializable]
public class NPCMove
{
    //Tooltip은 인스펙터 창에서 마우스오버 시 나오는 부가설명
    [Tooltip("NPCmove가 true일시 NPC가 움직임")]
    public bool Npcmove;
    public string[] direction; // npc가 움직일 방향 설정

    [Range(1, 5)] [Tooltip("1 = 천천히, 2 = 조금천천히, 3 = 보통, 4 = 빠르게, 5 = 연속적으로")]
    //[Range(1, 5) : frequency가 인스펙터 창에 1~5까지 조절할 수 있는 스크롤바가 나옴
    public int frequency; //npc가 얼마나 지정된 방향으로 빈번하게 움직일 것인가
}

public class Npcmanager : 변수저장소
{
    //커스텀 class가 인스펙터 창에 나오게 하기위한 명령어
    [SerializeField]
    public NPCMove npc;
    //커스텀class를 변수로 초기화(스크립트 초기화와 사용방법이 같음)

    public LayerMask layermask;
    public RaycastHit2D raycasthit;

    private void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void NpcMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {

    }

    //IEnumerator은 코루틴(Coroutine) 중에 하나로 yield return 구문을 어디엔가 포함하고 있는 함수이다.
    //yield return 구문을 포함하지 않으면 에러가 난다.
    //yield return : 실행을 중지하고 다음 프레임에서 실행을 재개할 수 있는 지점
    //코루틴을 실행하려면 StartCoroutine 함수를 사용하면 됨
    //코루틴 기본코드 IEnumerator 함수명(){code} 
    //코루틴 사용코드 StartCoroutine(코루틴함수명());
    IEnumerator MoveCoroutine()
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
                        //yield return new WaitForSeconds(); 값이 없을 경우 대기시간없이 무한히 함수가 반복되므로 렉걸려서 튕김
                        break;
                }

                yield return new WaitUntil(() => NpcCanMove);
                //NpcCanMove가 true가 될 때까지 무한대기
                //case5의 경우 대기시간이 없어 무한반복되어 렉걸리는데 코르틴이 끝날때마다 NpcCanMove를 true로 만들고 끝날때마다 
                //실행시키기 때문에 캐릭터는 딜레이없이 움직이면서 유니티도 안튕김
                base.Move(npc.direction[i], npc.frequency);
                // base : 자식이 상속받는 부모가 있을 시 부모의 메서드를 사용할 때 사용하는 접근자 ex) base.부모매서드명()
                // base는 앞에 붙이든 말든 상관없음 하지만 문제가 생길 때 보기편함

                //NPC가 무한반복하여 움직이게 하는 code
                if (i == npc.direction.Length - 1 && gameObject.name == "실험체") // i는 0부터 시작하므로 배열의 크기에 -1을 함
                    i = -1;
            }
        }
    }

    private void FixedUpdate()
    {
        //Ray에 콜라이더가 걸리면 안움직임
        Vector2 start;
        Vector2 end;
        start = transform.position;
        end = start + new Vector2(방향.x * speed * walkcount, 방향.y * speed * walkcount);

        RaycastHit2D rayhit = Physics2D.Linecast(start, end, layermask);
        if (rayhit.transform != null)
            NPCdontmove = true;
    }
}
