using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    //안쓰는 스크립트
    public float Speed;

    public float Runspeed;
    private float ApplyRunspeed;
    private bool CanRun = false;

    public int Walkcount;
    private int Currentwalkcount; //0.8 * 20 = 16 방향키 한번에 타일 하나씩 움직임

    private bool Canmove = true;

    Vector3 vector;

    IEnumerator playerCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ApplyRunspeed = Runspeed;
            CanRun = true;
        }
        else
        {
            ApplyRunspeed = 0;
            CanRun = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);//방향에 따라 각각 -1,1을리턴

        while (Currentwalkcount < Walkcount)
        {
            if (vector.x != 0)
                transform.Translate(vector.x * (Speed + ApplyRunspeed), 0, 0);
            else if (vector.y != 0)
                transform.Translate(0, vector.y * (Speed + ApplyRunspeed), 0);
            //Translate() : 현재 있는 값에서 ()안에 값만큼 더하는 함수
            if (CanRun)
                Currentwalkcount++;
            Currentwalkcount++;
            yield return new WaitForSeconds(0.01f);
        }
        Currentwalkcount = 0;
        Canmove = true;
    }

    private void Update()
    {
        if (Canmove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)//방향키를 입력받았을 때
            {
                Canmove = false;
                StartCoroutine(playerCoroutine());
            }
        }
    }
}
