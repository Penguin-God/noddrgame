using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Button button;
    public Gamemanager gamemanager;
    public Npcmanager npcmanager;

    public void EventOne()
    {
        StartCoroutine(HomeCoroutine());
    }

    IEnumerator HomeCoroutine()
    {
        yield return new WaitUntil(() => button);
        Debug.Log("asf");
        gamemanager.Talk(500, false);

        yield return new WaitForSeconds(0.1f);
    }
}
