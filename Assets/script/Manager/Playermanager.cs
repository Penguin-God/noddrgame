using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour 대신에 상속받고 싶은 script의 이름을 쓰면 상속이 됨
public class Playermanager : 변수저장소 //변수저장소 script를 상속받고 있음
{
    public Button button;

    GameObject TalkObject;
    Vector3 vector;
    private Animator animator;

    private int Xani;
    private int Yani;
    private float h;
    private float v;
    private bool XMove;
    public string currentmapname; //Scenechange script에 있는 mapname변수를 저장

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
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

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (gamemanager.isaction)
                animator.SetBool("Walking", false);
            else
            {
                vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);//방향에 따라 각각 -1,1을리턴
                if (Input.GetAxisRaw("Vertical") != 0)  
                    Yani++;
                if (Input.GetAxisRaw("Horizontal") != 0)
                    Xani++;

                if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0) // XAni, YAni값이 축적되지 않게 하기위한 코드
                {
                    if (Xani > Yani)
                    {
                        Xani = 6;
                        Yani = 0;
                    }
                    else if (Yani > Xani)
                    {
                        Yani = 6;
                        Xani = 0;
                    }
                }

                if (Xani > Yani + 5 && Input.GetAxisRaw("Vertical") != 0) // X축으로 움직이고 있다가 Y축 버튼을 누르면 vector.x값은 0
                    vector.x = 0;
                else if (Xani + 5 < Yani && Input.GetAxisRaw("Horizontal") != 0)
                    vector.y = 0;

                animator.SetFloat("DirX", vector.x); //DirX에 vector.x의 값을 받겠다.
                animator.SetFloat("DirY", vector.y);
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            Xani = 0;
            Yani = 0;
        }

        //ray 생성
        if (vector.y == 1)
            방향 = Vector3.up;
        if (vector.y == -1)
            방향 = Vector3.down;
        if (vector.x == 1)
            방향 = Vector3.right;
        if (vector.x == -1)
            방향 = Vector3.left;

        if (Input.GetButtonDown("Jump"))
        {
            if (TalkObject != null)
                gamemanager.오브젝트정보확인(TalkObject);
            if (gamemanager.isaction && button.cuthome)
                gamemanager.컷씬대화(button.cutnumber, false);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = XMove ? new Vector2(h, 0) : new Vector2(0, v);
        Rigidbody.velocity = moveVec * speed;

        Debug.DrawRay(Rigidbody.position, 방향 * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(Rigidbody.position, 방향, 0.7f, LayerMask.GetMask("Object"));

        //GameObject 변수는 null이 되면 인스펙터에서 None표시 안뜨고 그냥 전에 가져온 오브젝트가 빈 껍데기처럼 남아있는듯 함.
        if (rayhit.collider != null && !gamemanager.isaction)//대화중이 아닐때만 rayhit에 걸린 오브젝트 가져오기(NPC와 대화중에 다른 오브젝트를 가져오는 것을 방지하기 위함) 
            TalkObject = rayhit.collider.gameObject;
        else if(!gamemanager.isaction)
            TalkObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision) //트리거와 충돌시 충돌 오브젝트 가져오기
    {
        if (collision.gameObject.name != null && collision.gameObject.tag == "NpcTalk")
        {
            TalkObject = collision.gameObject;
            gamemanager.오브젝트정보확인(TalkObject);
        }
        else
            TalkObject = null;
    }
}


