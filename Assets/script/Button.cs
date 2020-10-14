using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public Mapchange mapchange;
    public GameObject 시작창;
    public GameObject 타이틀;

    public Playermanager playermanager;
    public Gamemanager gamemanager;
    public Test test;

    public bool cuthome;
    public int cutnumber;
    
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

        StartCoroutine(HomeCoroutine());
    }

    IEnumerator HomeCoroutine()
    {
        cuthome = true;
        yield return new WaitForSeconds(1f);
        gamemanager.컷씬대화(600);

        yield return new WaitUntil(() => !playermanager.isaction);
        test.NpcMove();
        playermanager.isaction = true;
        cuthome = false;
        yield return new WaitUntil(() => !playermanager.isaction);
        mapchange.CutMapChange();
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }
}
