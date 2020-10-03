using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraTest : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Awake()
    {
        transform.position = target.position - offset;
    }
}
