using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapchange : MonoBehaviour
{
    public PolygonCollider2D polygon;
    public Cameramanager cameramanager;
    public Transform MapChangePoint;
    //private 변수저장소 Theplayer;
    private Fademanager fademanager;
    private Playermanager playermanager;

    private void Start()
    {
        //FindObjectOfType : 유니티 계층에 있는 모든 객체를 참조해서 가져옴 Getcomponent와 검색 범위가 다름
        fademanager = FindObjectOfType<Fademanager>();
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
    public void CutMapChange()
    {
        StartCoroutine(MapTransCoroutine());
    }

    IEnumerator MapTransCoroutine() //맵이 어두워지고 이동한 후 다시밝아지는 코루틴
    {
        playermanager.isaction = true;
        fademanager.FadeOut();
        yield return new WaitForSeconds(1f);
        cameramanager.CollderChange(polygon); 
        playermanager.transform.position = MapChangePoint.transform.position;  // Transform 변수에 게임 오브젝트를 넣어 충돌 시 그 오브젝트 위치로 이동하게 함 
        yield return new WaitForSeconds(0.5f);
        fademanager.FadeIn();
        yield return new WaitForSeconds(0.5f);
        playermanager.isaction = false;
    }
}
