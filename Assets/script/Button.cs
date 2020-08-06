using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;
    public bool GameOn;

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
        GameOn = true;
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }
}
