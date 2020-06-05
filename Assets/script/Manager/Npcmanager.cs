using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커스텀 class가 인스펙터 창에 나오게 하기위한 명령어
[System.Serializable]
public class NPCMove
{
    //Tooltip은 인스펙터 창에서 마우스오버 시 나오는 부가설명
    [Tooltip("NPCMove가 true일시 NPC가 움직임")]
    public bool Npcmove;
    public string[] direction; // npc가 움직일 방향 설정

    [Range(1, 5)] [Tooltip("1 = 천천히, 2 = 조금천천히, 3 = 보통, 4 = 빠르게, 5 = 연속적으로")]
    public int frequency; //npc가 얼마나 지정된 방향으로 빈번하게 움직일 것인가
}

public class Npcmanager : MonoBehaviour
{
    //커스텀 class가 인스펙터 창에 나오게 하기위한 명령어
    [SerializeField]
    public NPCMove npc;

    public void SetMove()
    {

    }

    public void SetNotMove()
    {

    }


    //IEnumerator은 코루틴 중에 하나로 yield return 구문을 어디엔가 포함하고 있는 함수이다.
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i < npc.direction.Length; i++)
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
            }
        }
    }
}
