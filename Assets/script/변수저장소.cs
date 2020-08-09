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
    public bool NPCdontmove;
    private bool NotCortoutine;

    public int walkcount;
    protected int count;
    public string CharactreName;

    public Queue<string> queue;
    //Queue : 선입선출 자료구조(먼저넣은값이 가장 앞에있고 값을 뺄 때 가장 앞에 있는게 먼저 빼지는 자료구조)
    //Queue자료구조에 a,b,c 값을 넣으면 a,b,c 순서대로 값이 들어가고 값을 뻬려고 하면 a,b,c순서대로 값이 나온다.
    //Enqueue() : Queue의 끝 부분에 값을 넣는 것, Dequeue : Queue 의 시작 부분에서 개체를 제거하고 반환함. 

    //protected : 부모자식간의 상속은 가능하지만 인스펙터 창에서 노출은 되지않는 보호수준
    protected Rigidbody2D Rigidbody;
    protected Vector3 방향;

    public void Move(string dir, int frequencey = 5)
    {
        queue.Enqueue(dir); //queue에 dir을 넣음
        if (!NotCortoutine) 
        {
            //지정한 방향값이 다수일 경우 코루틴이 다중반복실행되어 오브젝트가 개빨리 움직이는 에러가 나서 코루틴 시작 조건을 만듬
            StartCoroutine(MoveCoroutine(dir, frequencey));
            NotCortoutine = true;
        }
    }

    IEnumerator MoveCoroutine(string dir, int frequencey)
    {
        while(queue.Count != 0) //queue의 값이 모두 제거되면 멈춤
        {
            string direction = queue.Dequeue(); //queue에서 제거되는 값이 switch문의 값이됨
            방향.Set(0, 0, 방향.z);//코루틴을 한번 돌고나면 백터값을 초기화(x,y가 동시에 1을가지면 안되기 때문)

            //switch 문에는 string값이 와도 상관이 없음
            switch (direction)
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
        }
        NotCortoutine = false;//while문이 끝난후 다시 false로 바꿔 코루틴이 돌아가게함
    }
}


