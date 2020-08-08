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
    public bool NPCdontmove;
    public int walkcount;

    protected int count;

    //protected : 부모자식간의 상속은 가능하지만 인스펙터 창에서 노출은 되지않는 보호수준
    protected Rigidbody2D Rigidbody;
    protected Vector3 방향;

    protected void Move(string dir, int frequencey = 5)
    {
        StartCoroutine(MoveCoroutine(dir, frequencey));
    }

    IEnumerator MoveCoroutine(string dir, int frequencey)
    {
        NpcCanMove = false;
        방향.Set(0, 0, 방향.z);//코루틴을 한번 돌고나면 백터값을 초기화(x,y가 동시에 1을가지면 안되기 때문)

        //switch 문에는 string값이 와도 상관이 없음
        switch (dir)
        {
            case ("UP"):
                방향.y = 1f;
                break;
            case ("DOWN"):
                방향.y = -1f;
                break;
            case ("RIGHT"):
                방향.x = 1f;
                break;
            case ("LEFT"):
                방향.x = -1f;
                break;
            case ("NONE"):
                방향.x = 0;
                방향.y = 0;
                break;
        }

        while (true)
        {
            if (NPCdontmove)
                yield return new WaitForSeconds(1f);
            else
                break;
        }

        while (count < walkcount)
        {
            transform.Translate(방향.x * speed, 방향.y * speed, 0);
            //코루틴에서 백터값을 초기화하기 때문에 x,y를 동시에 움직여도 대각선 이동은 일어나지 않음
            count++;
            yield return new WaitForSeconds(0.01f);
        }
        count = 0;
        NpcCanMove = true;
    }
}


