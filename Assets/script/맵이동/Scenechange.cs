using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 라이브러리 가져오기

public class SceneChange : MonoBehaviour
{
    public string sceneName; // 이동할 씬의 이름
    private Playermanager playermanager;

    private void Start()
    {
        playermanager = FindObjectOfType<Playermanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            playermanager.currentMapname = sceneName; // 씬 이동시 출발한 씬을 currentMapname에 저장
            SceneManager.LoadScene(sceneName);
        }
    }
}
