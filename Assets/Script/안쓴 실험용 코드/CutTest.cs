using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTest : MonoBehaviour
{
    public Cameramanager cameramanager;
    public PlayerStat stat;
    public Gamemanager gamemanager;
    public Fademanager fademanager;

    private Animator animator;

    private void Awake()
    {
        animator = stat.GetComponent<Animator>();
    }

    public void StartCut(float speed)
    {
        StartCoroutine(GameStartCut(speed));
    }

    IEnumerator GameStartCut(float Speed)
    {
        fademanager.UIFadeIn(Speed);
        yield return new WaitUntil(() => fademanager.color.a < 0.4f);
        // 대사 시작
        gamemanager.CutSceneTalk(700);
        for (int i = 0; i < 2; i++)
        {
            int index = gamemanager.talkindex;
            cameramanager.CameraMove(new Vector3(0, 4.5f, 0), 0.1f, 15);
            stat.CurrentHp += 24;
            animator.SetBool("testDDR", true);
            yield return new WaitUntil(() => !cameramanager.isCameraMove && index != gamemanager.talkindex);
        }
        animator.SetBool("testDDR", false);
        stat.CurrentHp += 2;
        yield return new WaitForSeconds(1.4f);
        gamemanager.CutSceneTalk(800);
    }
}
