using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 변수저장소 : MonoBehaviour
{
    public Gamemanager gamemanager;
    public string currentmapname; //Scenechange script에 있는 mapname변수를 저장
    public float speed;
    protected float h;
    protected float v;
    protected bool XMove;
    protected bool NpcCanMove = true;

    public int walkcount;
    protected int count;

    //protected : 부모자식간의 상속은 가능하지만 인스펙터 창에서 노출은 되지않는 보호수준
    protected Rigidbody2D Rigidbody;
    protected Vector3 방향;
}


