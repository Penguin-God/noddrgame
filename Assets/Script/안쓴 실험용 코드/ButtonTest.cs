using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;
    public GameObject virtualCamera;

    public Cameramanager cameramanager;
    public PlayerStat stat;
    public Playermanager playermanager;
    public Gamemanager gamemanager;
    public Fademanager fademanager;
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

        StartCoroutine(GameStartCut(0.005f));
    }

    public void StartCancel()
    {
        audioManager.WalkAudioPlay(false);
        시작창.SetActive(false);
    }

    IEnumerator GameStartCut(float Speed)
    {
        fademanager.UIFadeIn(Speed);
        yield return new WaitUntil(() => fademanager.color.a < 0.4f);
        // 대사 시작
        gamemanager.CutSceneTalk(700);
        for(int i = 0; i < 2; i++)
        {
            int index = gamemanager.talkindex; 
            cameramanager.CameraMove(new Vector3(0, 4.5f, 0), 0.1f, 15);
            stat.CurrentHp += 24;
            yield return new WaitUntil(() => !cameramanager.isCameraMove && index != gamemanager.talkindex);
        }
        stat.CurrentHp += 2;
        yield return new WaitForSeconds(1.4f);
        gamemanager.CutSceneTalk(800);
    }
}

