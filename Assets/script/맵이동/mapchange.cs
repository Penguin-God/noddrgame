using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapchange : MonoBehaviour
{
    public Fademanager fademanager;
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
        fademanager.FadeOut();
        yield return new WaitForSeconds(1f);
        playermanager.transform.position = MapChangePoint.transform.position;  // Transform 변수에 게임 오브젝트를 넣어 충돌 시 그 오브젝트 위치로 이동하게 함 
        yield return new WaitForSeconds(0.5f);
        camera.transform.position = new Vector3(playermanager.transform.position.x, playermanager.transform.position.y, -10);
        // cameramanager.CollderChange(); // 위치변경 후 카메라 제한구역 여부 확인 및 적용
        fademanager.FadeIn();
        yield return new WaitForSeconds(0.5f);
        playermanager.isaction = false;
    }
}
