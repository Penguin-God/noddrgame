using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramanager : MonoBehaviour
{
    public bool isCameraMove;

    public void CameraMove(Vector3 MoveVec, float MoveTime, int MoveCount)
    {
        StartCoroutine(CameraMoveCoroutine(MoveVec, MoveTime, MoveCount));
    }

    IEnumerator CameraMoveCoroutine(Vector3 MoveVec, float MoveTime, int MoveCount)
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
