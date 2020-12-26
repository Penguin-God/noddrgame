using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;

    public CutScenes cutScenes;
    public AudioManager audioManager;

    public void GameExit() // 게임종료
    {
        Application.Quit();
    } 

    public void 시작창뛰우기()
    {
        시작창.SetActive(true);
        audioManager.WalkAudioPlay(false);
    }

    public void GameStart()
    {
        타이틀.SetActive(false);
        시작창.SetActive(false);
        audioManager.WalkAudioPlay(false);

        cutScenes.StartCut(0.002f);
    }

    public void StartCancel()
    {
        audioManager.WalkAudioPlay(false);
        시작창.SetActive(false);
    }
}

