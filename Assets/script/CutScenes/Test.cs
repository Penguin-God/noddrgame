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

    //public Eventmanager eventmanager;
    public Npcmanager npcmanager;

    public void TestMove(string name, string dir)
    {
        if (name == npcmanager.CharactreName)
        {
            npcmanager.Move(dir);
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
}
