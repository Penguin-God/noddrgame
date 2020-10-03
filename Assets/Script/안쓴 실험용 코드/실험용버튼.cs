﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class 실험용버튼 : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;
    public GameObject virtualCamera;

    public Camera Camera;
    public PlayerStat stat;
    public Playermanager playermanager;
    public Gamemanager gamemanager;

    public void GameExit() // 게임종료
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

        StartCoroutine(HpAdd());
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }

    IEnumerator HpAdd()
    {
        gamemanager.컷씬대화(700, false);
        while (stat.CurrentHp < stat.maxHp)
        {
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
            stat.CurrentHp += 17;
            Camera.transform.position += new Vector3(0, 3, 0);
            yield return new WaitForSeconds(0.05f);
        }
        virtualCamera.SetActive(true);
    }
}
