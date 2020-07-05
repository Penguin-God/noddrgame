using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapchange : MonoBehaviour
{
    public Transform MapChangePoint;
    private 변수저장소 Theplayer;

    private void Start()
    {
        //FindObjectOfType : 유니티 계층에 있는 모든 객체를 참조해서 가져옴 Getcomponent와 검색 범위가 다름
        Theplayer = FindObjectOfType<변수저장소>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            //Transform 변수에 게임 오브젝트를 넣어 충돌 시 그 오브젝트 위치로 이동하게 함 
            Theplayer.transform.position = MapChangePoint.transform.position;
        }
    }
}
