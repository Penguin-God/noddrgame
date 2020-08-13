﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;

    public Gamemanager gamemanager;
    public Eventmanager eventmanager;
    public Testmove[] testmove;
    
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
        cuthome = true;

        StartCoroutine(HomeCoroutine());
    }

    IEnumerator HomeCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gamemanager.컷씬대화(cutnumber, false);

        yield return new WaitUntil(() => !gamemanager.isaction);
        yield return new WaitForSeconds(1.5f);
        CutHome();
    }

    public void CutHome()
    {
        if (cuthome)
        {
            eventmanager.NpcLode();
            for (int i = 0; i < testmove.Length; i++)
            {
                eventmanager.EventMove(testmove[i].name, testmove[i].direction);
            }
            cuthome = false;
        }
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }
}
