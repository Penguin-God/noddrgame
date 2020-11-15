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

    public void ValueRotation(Vector3 MoveVec)
    {
        rect.Rotate(MoveVec);
    }
}
