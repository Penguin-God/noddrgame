using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 이동금지 : MonoBehaviour
{
    public Gamemanager gamemanager;
    GameObject scan;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            gamemanager.Action();
        }
    }
}
