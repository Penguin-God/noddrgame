using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenechangeposition : MonoBehaviour
{
    public string mapstartpoint; // 맵의 이동시 플레이어가 시작할 위치
    private 변수저장소 Theplayer; 

    private void Start()
    {
        Theplayer = FindObjectOfType<변수저장소>();

        if (mapstartpoint == Theplayer.currentmapname)//씬이 바뀔때마다 mapchangepoint의 mapstartpoint와 currentmapname가 같을시 
        {
            //플레이어의 위치를 이 스크립트를 가지고 있는 빈오브젝트의 위치로 바꿈
            Theplayer.transform.position = this.transform.position;
        }
    }
}
