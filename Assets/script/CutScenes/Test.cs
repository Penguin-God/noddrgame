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

    private Eventmanager eventmanager;

    private void Start()
    {
        eventmanager = FindObjectOfType<Eventmanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            eventmanager.NpcLode();
            for(int i = 0; i < testmove.Length; i++)
            {
                eventmanager.EventMove(testmove[i].name, testmove[i].direction);
            }
        }
    }
}
