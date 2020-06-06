using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    public string currentmapname; //Scenechange script에 있는 mapname변수를 저장
    public Gamemanager gamemanager;
    public float speed;
    protected float h;
    protected float v;
    protected bool XMove;
    protected bool NpcCanMove = true;

    public int walkcount;
    protected int count;

    //protected : 부모자식간의 상속은 가능하지만 인스펙터 창에서 노출은 되지않는 보호수준
    protected Rigidbody2D Rigid;
    protected Vector3 dirvec;

    protected void Move(string _dir)
    {
        StartCoroutine(MoveCoroutine(_dir));
    }

    IEnumerator MoveCoroutine(string _dir)
    {
        NpcCanMove = false;
        dirvec.Set(0, 0, dirvec.z);

        //switch 문에는 string값이 와도 상관이 없음
        switch (_dir)
        {
            case("UP"):
                dirvec.y = 1f;
                break;
            case ("DOWN"):
                dirvec.y = -1f;
                break;
            case ("RIGHT"):
                dirvec.x = 1f;
                break;
            case ("LEFT"):
                dirvec.x = -1f;
                break;
        }

        while(count < walkcount)
        {
            transform.Translate(dirvec.x * speed, dirvec.y * speed, 0);
            count++;
            yield return new WaitForSeconds(0.01f);
        }
        count = 0;
        NpcCanMove = true;
    }
}


