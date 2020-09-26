using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramanager : MonoBehaviour
{
    public PolygonCollider2D polygon;
    CinemachineConfiner Confiner;

    private void Awake()
    {
        Confiner = GetComponent<CinemachineConfiner>();
    }

    public void CollderChange(PolygonCollider2D newBox)
    {
        Confiner.m_BoundingShape2D = newBox; // 제한 콜라이더를 MpaChange스크립트가 가지고 있는 콜라이더로 변경
    }
}
