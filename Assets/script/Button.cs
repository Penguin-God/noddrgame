using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;

    public Gamemanager gamemanager;
    public Npcmanager npcmanager;

    public void GameExit()
    {
        Application.Quit();
    }

    public void 시작창뛰우기()
    {
        시작창.SetActive(true);
    }

    public void GameStart()
    {
        타이틀.SetActive(false);
        시작창.SetActive(false);
        StartCoroutine(CutCoroutine());
    }

    IEnumerator CutCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gamemanager.컷씬대화(400, false);

        yield return new WaitForSeconds(3f);
        npcmanager.NpcMove();
    }
 
    public void StartCancel()
    {
        시작창.SetActive(false);
    }
}
