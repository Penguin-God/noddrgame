using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapchange : MonoBehaviour
{
    public Transform MapChangePoint;
    //private 변수저장소 Theplayer;
    private Fademanager fademanager;
    private Gamemanager gamemanager;
    private Playermanager playermanager;

    private void Start()
    {
        //FindObjectOfType : 유니티 계층에 있는 모든 객체를 참조해서 가져옴 Getcomponent와 검색 범위가 다름
        //Theplayer = FindObjectOfType<변수저장소>();
        fademanager = FindObjectOfType<Fademanager>();
        gamemanager = FindObjectOfType<Gamemanager>();
        playermanager = FindObjectOfType<Playermanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            StartCoroutine(MapTransCoroutine());
        }
    }

    //컷씬 시 캐릭터가 이동해야 할 경우 사용
    public void EventMove()
    {
        StartCoroutine(MapTransCoroutine());
    }

    IEnumerator MapTransCoroutine() //맵이 어두워지고 이동한 후 다시밝아지는 코루틴
    {
        gamemanager.isaction = true;
        fademanager.FadeOut();
        yield return new WaitForSeconds(1f);

        playermanager.transform.position = MapChangePoint.transform.position;
        //Transform 변수에 게임 오브젝트를 넣어 충돌 시 그 오브젝트 위치로 이동하게 함 

        fademanager.FadeIn();
        yield return new WaitForSeconds(0.5f);
        gamemanager.isaction = false;
    }
}
