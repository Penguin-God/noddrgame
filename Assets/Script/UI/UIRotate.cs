using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void ValueRotation(Vector3 MoveVec) // MoveVec만큼 UI를 회전시키는 함수
    {
        rect.Rotate(MoveVec);
    }
}
