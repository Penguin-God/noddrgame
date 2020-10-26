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

    public Image BlackImg;
    private Color color;
    private WaitForSeconds UIOutTime = new WaitForSeconds(0.01f);

    public Camera Camera;
    public PlayerStat stat;
    public Playermanager playermanager;
    public Gamemanager gamemanager;

    private void Awake()
    {
        color = BlackImg.color;
    }

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

        StartCoroutine(GameStartCut(0.005f));
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }


    IEnumerator GameStartCut(float Speed)
    {
        while (color.a > 0f) // 검은창 천천히 투명하게 하는 코드
        {
            color.a -= Speed;
            BlackImg.color = color;
            yield return UIOutTime;
        }

        // 대사 시작
        gamemanager.컷씬대화(700);
        while (!stat.PlayerDie)
        {
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
            stat.CurrentHp += 17;
            Camera.transform.position += new Vector3(0, 3, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitUntil(()=> stat.CurrentHp == 0);
        gamemanager.컷씬대화(800);
    }
}

