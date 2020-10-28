﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour 대신에 상속받고 싶은 script의 이름을 쓰면 상속이 됨
public class Playermanager : 변수저장소 //변수저장소 script를 상속받고 있음
{
    public AudioManager audioManager;

    public GameObject ManagerParents;
    public GameObject CameraParents;

    Vector2 PlayerVector;
    Vector2 RayVector;
    GameObject TalkObject;
    Animator animator;

    private int xMove;
    private int yMove;
    
    public bool isaction;
    bool isRun;

    public string currentMapname; // SceneChange script에 있는 sceneName 변수값을 할당받음

    private void Awake() // Awake() : Start()함수와 다르게 script가 비활성화 상태여도 실해됨 즉 Start()함수는 비활성화 상태일시 script가 활성화되어야 실행됨
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        Move();
        Ray();
        대화();
    }

    void GetInput()
    {
        isRun = Input.GetButton("Run");
    }

    void Move()
    {
        if (isaction)
        {
            PlayerVector.x = 0;
            PlayerVector.y = 0;
            animator.SetBool("Walking", false);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            PlayerVector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Vector에 따라 각각 -1,1을리턴
            if (Input.GetAxisRaw("Vertical") != 0)
                yMove++;
            if (Input.GetAxisRaw("Horizontal") != 0)
                xMove++;
            if (xMove == yMove) // x,y축 동시에 누를 때 대각선 이동 방지
                return;

            // xMove를 오랫동안 눌러서 값을 올라가면 수직이동중에 방향전환이 되지않는 버그때문에 XAni, YAni값이 축적되지 않게 하기위한 코드
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                // X축과 Y축을 동시에 이동 시 전에 이동하던 방향의 ani변수가 더 높도록 조정하여 방향전환이 일어나게 함
                if (xMove > yMove)
                {
                    xMove = 5;
                    yMove = 0;
                }
                else if (yMove > xMove)
                {
                    yMove = 5;
                    xMove = 0;
                }

                if (xMove > yMove && Input.GetAxisRaw("Vertical") != 0) // X축으로 움직이고 있다가 Y축 버튼을 누르면 방향.x값은 0 즉 수평이동중에 수직으로 방향전환 
                    PlayerVector.x = 0;
                else if (xMove < yMove && Input.GetAxisRaw("Horizontal") != 0)
                    PlayerVector.y = 0;
            }
            RayVector = PlayerVector; // Ray
            // Animation
            animator.SetFloat("DirX", PlayerVector.x); 
            animator.SetFloat("DirY", PlayerVector.y);
            animator.SetBool("Walking", true);

            audioManager.WalkAudioPlay(isRun);
        }
        else // 가만히 있을 때 
        {
            animator.SetBool("Walking", false);
            xMove = 0;
            yMove = 0;
            PlayerVector = Vector2.zero;
        }
        // Move : 위에서 적용한 백터값을 이용해 이동
        Rigidbody.velocity = PlayerVector * speed * (isRun ? 2f : 1f); // velocity(속도) : 리지드바디의 속도 벡터로 Rigidbody 위치의 변화율을 나타냄.
    }

    void Ray()
    {
        Debug.DrawRay(Rigidbody.position, RayVector * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(Rigidbody.position, RayVector, 0.7f, LayerMask.GetMask("Object")); // 레이어가 Object인 물체만 감지함 

        // GameObject 변수는 null이 되면 인스펙터에서 None표시 안뜨고 그냥 전에 가져온 오브젝트가 빈 껍데기처럼 남아있는듯 함.
        if (rayhit.collider != null && !isaction)// 대화중이 아닐때만 rayhit에 걸린 오브젝트 가져오기(NPC와 대화중에 다른 오브젝트를 가져오는 것을 방지하기 위함) 
            TalkObject = rayhit.collider.gameObject;
        else if (!isaction)
            TalkObject = null;
    }

    void 대화()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (TalkObject != null)
                gamemanager.오브젝트정보확인(TalkObject);
            else if (isaction && gamemanager.CutNumber != 0)
                gamemanager.컷씬대화(gamemanager.CutNumber);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 트리거와 충돌시 충돌 오브젝트 정보 가져오기 : Npc충돌 시 대화에 사용
    {
        if (collision.gameObject.tag == "NpcTalk")
        {
            TalkObject = collision.gameObject;
            gamemanager.오브젝트정보확인(TalkObject);
        }
        else
            TalkObject = null;
    }
}


