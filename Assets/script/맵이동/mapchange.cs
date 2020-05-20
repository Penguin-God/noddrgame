using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapchange : MonoBehaviour
{
    public Transform door;
    private play Theplayer;

    private void Start()
    {
        Theplayer = FindObjectOfType<play>();
        //FindObjectOfType : 유니티 계층에 있는 모든 객체를 참조해서 가져옴 Getcomponent와 검색 범위가 다름
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            Theplayer.transform.position = door.transform.position;
        }
    }
}
