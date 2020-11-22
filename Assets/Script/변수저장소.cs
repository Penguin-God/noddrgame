using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 변수저장소 : MonoBehaviour
{
    public Gamemanager gamemanager;

    public float speed;

    public string CharactreName;

    //protected : 부모자식간의 상속은 가능하지만 인스펙터 창에서 노출은 되지않는 보호수준
    protected Rigidbody2D Rigidbody;
}


