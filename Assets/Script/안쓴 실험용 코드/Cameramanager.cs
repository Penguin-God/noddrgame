using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramanager : MonoBehaviour
{
    public CinemachineConfiner Confiner;
    public bool isCameraMove;

    private void Awake()
    {
        Confiner = GetComponent<CinemachineConfiner>();
    }

    public void CollderChange(PolygonCollider2D newBox) 
    {
        if (Confiner.m_BoundingShape2D != newBox)
            Confiner.m_BoundingShape2D = newBox;
    }

    public void CameraMove(Vector3 MoveVec, float MoveTime, int MoveCount)
    {
        StartCoroutine(CameraMoveCo(MoveVec, MoveTime, MoveCount));
    }

    IEnumerator CameraMoveCo(Vector3 MoveVec, float MoveTime, int MoveCount)
    {
        isCameraMove = true;
        for (int i = 0; MoveCount > i; i++)
        {
            this.transform.position += MoveVec / MoveCount;
            yield return new WaitForSeconds(MoveTime);
        }
        isCameraMove = false;
    }
}
