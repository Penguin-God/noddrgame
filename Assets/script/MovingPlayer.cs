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
    private Animator animator; 

    private void Start()
    {
        animator = GetComponent<Animator>(); //animator변수에 오브젝트에 있는 애니메이터를 담는 것 이것을 하지않으면 animator변수는 그냥 빈 껍데기
    }

    IEnumerator PlayerCoroutine()
    {
        //Update에서만 입력받으면 프레임에 따라 애니메이션이 입력했다가 마지막에 끊기기 때문에 코루틴 내에서 while문을 넣어 자연스럽게 만듬
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) 
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

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x); //DirX에 vector.x의 값을 받겠다.
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

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
        }
        animator.SetBool("Walking", false);
        Canmove = true;
    }

    private void Update()
    {
        if (Canmove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)//방향키를 입력받았을 때
            {
                Canmove = false;
                StartCoroutine(PlayerCoroutine());
            }
        }
    }
}
