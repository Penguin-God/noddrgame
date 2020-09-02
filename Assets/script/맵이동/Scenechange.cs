using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    public string Scenename; // 이동할 씬의 이름
    private Playermanager playermanager;

    private void Start()
    {
        //FindObjectOfType : 유니티 계층에 있는 모든 객체를 참조해서 가져옴 Getcomponent와 검색 범위가 다름
        playermanager = FindObjectOfType<Playermanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            playermanager.currentmapname = Scenename; //이동한 씬을 play script에 저장함
            SceneManager.LoadScene(Scenename);
        }
    }
}
