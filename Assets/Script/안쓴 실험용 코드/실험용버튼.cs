using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class 실험용버튼 : MonoBehaviour
{
    public GameObject 시작창;
    public GameObject 타이틀;

    public PlayerStat stat;
    public Playermanager playermanager;
    public Gamemanager gamemanager;

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

        StartCoroutine(HpAdd());
    }

    public void StartCancel()
    {
        시작창.SetActive(false);
    }

    IEnumerator HpAdd()
    {
        playermanager.isCut = true;
        gamemanager.컷씬대화(700, false);
        playermanager.CutNumber = 700;
        playermanager.isaction = true;
        while(stat.CurrentHp < stat.maxHp)
        {
            yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
            stat.CurrentHp += 17;
            yield return new WaitForSeconds(0.05f);
        }
        playermanager.isaction = false;
        playermanager.isCut = false;
    }
}
