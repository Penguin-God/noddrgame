using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Testmove
{
    public string name;
    public string direction;
}

public class Test : MonoBehaviour
{
    [SerializeField]
    public Testmove[] testmove;

    public Npcmanager[] npcmanager;

    public void OneMove(string name, string dir) // 한번 이동하는 함수
    {
        for (int i = 0; i < npcmanager.Length; i++)
        {
            if (name == npcmanager[i].CharactreName)
            {
                npcmanager[i].Move(dir);
            }
        }
    }
    public void NpcMove() // TestMove를 배열수만큼 돌리는 함수
    {
        for (int i = 0; i < testmove.Length; i++)
        {
            OneMove(testmove[i].name, testmove[i].direction);
        }
    }


    public void ObjectSetFalse(string name) //원하는 게임 오브젝트를 숨기는 함수
    {
        for (int i = 0; i < npcmanager.Length; i++)
        {
            if (name == npcmanager[i].CharactreName)
            {
                npcmanager[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 콜라이더에 플레이어 닿을 시 이동
    {
        if (collision.gameObject.name == "player")
            NpcMove();
    }
}
