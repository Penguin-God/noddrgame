using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramanager : MonoBehaviour
{
    public CinemachineConfiner Confiner;

    private void Awake()
    {
        Confiner = GetComponent<CinemachineConfiner>();
    }

    public void CollderChange(PolygonCollider2D newBox) 
    {
        if (Confiner.m_BoundingShape2D != newBox)
            Confiner.m_BoundingShape2D = newBox;
    }
}
