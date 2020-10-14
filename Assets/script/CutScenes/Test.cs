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

    public void TestMove(string name, string dir)
    {
        for(int i = 0; i < npcmanager.Length; i++)
        {
            if (name == npcmanager[i].CharactreName)
            {
                npcmanager[i].Move(dir);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 내가 구현한 이동
    {
        if (collision.gameObject.name == "player")
        {
            for (int i = 0; i < testmove.Length; i++)
            {
                TestMove(testmove[i].name, testmove[i].direction);
            }
        }
    }

    public void NpcMove()
    {
        for (int i = 0; i < testmove.Length; i++)
        {
            TestMove(testmove[i].name, testmove[i].direction);
        }
    }

    public void ObjectSetFalse(string name) //원하는 게임 오브젝트를 숨기는 함수
    {
        for(int i = 0; i < npcmanager.Length; i++)
        {
            if (name == npcmanager[i].CharactreName)
            {
                npcmanager[i].gameObject.SetActive(false);
            }
        }
    }
}
