using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 라이브러리 가져오기

public class SceneChange : MonoBehaviour
{
    public string sceneName; // 이동할 씬의 이름
    public Playermanager playermanager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            playermanager.currentMapname = sceneName; //이동한 씬을 play script에 저장함
            SceneManager.LoadScene(sceneName);
        }
    }
}
