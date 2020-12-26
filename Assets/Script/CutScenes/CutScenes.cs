using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenes : MonoBehaviour
{
    public Cameramanager cameramanager;
    public PlayerStat playerStat;
    public Gamemanager gamemanager;
    public Fademanager fademanager;
    public Playermanager playermanager;

    private Animator animator;

    private void Awake()
    {
        animator = playerStat.GetComponent<Animator>();
    }

    public void StartCut(float speed)
    {
        StartCoroutine(GameStartCut(speed));
    }

    IEnumerator GameStartCut(float Speed)
    {
        fademanager.UIFadeIn(Speed);
        animator.SetBool("testDDR", true);
        yield return new WaitUntil(() => fademanager.color.a < 0.6f);
        // 대사 시작
        gamemanager.CutSceneTalk(700);
        yield return new WaitUntil(() => !playermanager.isaction);
        StartCoroutine(playerStat.AddHp(4, 0.15f));
        yield return new WaitUntil(() => playerStat.PlayerDie);
        animator.SetBool("testDDR", false);
        gamemanager.CutSceneTalk(800);
    }

    // 대사 넘어갈 때마다 카메라 이동시키는 코드
    //for (int i = 0; i < 2; i++)
    //{
    //    int index = gamemanager.talkindex;
    //    cameramanager.CameraMove(new Vector3(0, 4.5f, 0), 0.1f, 15);
    //    yield return new WaitUntil(() => index != gamemanager.talkindex);
    //}
}
