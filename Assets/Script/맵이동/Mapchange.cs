using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapchange : Fademanager
{
    public Playermanager playermanager;

    //public Cameramanager cameramanager;
    public new Camera camera;
    public Transform MapChangePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            StartCoroutine(MapTransCoroutine());
        }
    }

    public void CutMapChange() //컷씬 시 캐릭터가 이동해야 할 경우 사용
    {
        StartCoroutine(MapTransCoroutine());
    }

    IEnumerator MapTransCoroutine() //맵이 어두워지고 맵 이동 후 다시밝아지는 코루틴
    {
        playermanager.isaction = true;
        BlackIn();
        yield return new WaitUntil(() => color.a >= 0.99);
        playermanager.transform.position = MapChangePoint.transform.position;  // Transform 변수에 게임 오브젝트를 넣어 충돌 시 그 오브젝트 위치로 이동하게 함 
        camera.transform.position = new Vector3(playermanager.transform.position.x, playermanager.transform.position.y, -10);

        yield return new WaitForSeconds(0.5f);
        // cameramanager.CollderChange(); // 위치변경 후 카메라 제한구역 여부 확인 및 적용
        BlackOut();
        yield return new WaitForSeconds(0.5f);
        playermanager.isaction = false;
    }

    public IEnumerator FadeIn_and_Out() //맵이 어두워지고 맵 이동 후 다시밝아지는 코루틴
    {
        playermanager.isaction = true;
        BlackIn(0.006f);
        yield return new WaitUntil(() => color.a >= 0.99);
        yield return new WaitForSeconds(2f);
        BlackOut(0.003f);
        yield return new WaitForSeconds(1f);
        playermanager.isaction = false;
    }
}