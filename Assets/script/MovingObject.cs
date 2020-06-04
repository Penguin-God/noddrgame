using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Gamemanager gamemanager;
    public float speed;
    float h;
    float v;
    bool XMove;

    Rigidbody2D Rigid;
    Vector3 dirvec;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = gamemanager.isaction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gamemanager.isaction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = gamemanager.isaction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gamemanager.isaction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gamemanager.isaction ? true : Input.GetButtonUp("Horizontal");
        bool vUp = gamemanager.isaction ? true : Input.GetButtonUp("Vertical");

        //대각선 이동 차단
        if (hDown)
            XMove = true;
        else if (vDown)
            XMove = false;
        else if (hUp || vUp)
            XMove = h != 0;

        if (vDown && v == 1)
            dirvec = Vector3.up;
        if (vDown && v == -1)
            dirvec = Vector3.down;
        if (hDown && h == 1)
            dirvec = Vector3.right;
        if (hDown && h == -1)
            dirvec = Vector3.left;
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = XMove ? new Vector2(h, 0) : new Vector2(0, v);
        Rigid.velocity = moveVec * speed;
    }
}


