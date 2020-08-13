using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour 대신에 상속받고 싶은 script의 이름을 쓰면 상속이 됨
public class Playermanager : 변수저장소 //변수저장소 script를 상속받고 있음
{
    public Button button;

    GameObject TalkObject;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    //private void Start()
    //{
    //    queue = new Queue<string>();
    //}

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

        //ray 생성
        if (vDown && v == 1)
            방향 = Vector3.up;
        if (vDown && v == -1)
            방향 = Vector3.down;
        if (hDown && h == 1)
            방향 = Vector3.right;
        if (hDown && h == -1)
            방향 = Vector3.left;

        if (Input.GetButtonDown("Jump"))
        {
            if (TalkObject != null)
                gamemanager.오브젝트정보확인(TalkObject);
            if (gamemanager.isaction)
                gamemanager.컷씬대화(button.cutnumber, false);
        }

        //에러난 코드(첫번째 충돌 후 다시 가면 대사가 안뜨고 다시 충돌해야 대사가 뜸)
        //else if (gamemanager.isaction && Input.GetButtonDown("Jump"))
        //{
        //    gamemanager.isaction = false;
        //    gamemanager.talkwindow.SetActive(gamemanager.isaction);
        //    Debug.Log("asfg");
        //}
    }

    private void FixedUpdate()
    {
        //Time.time : 이번프레임이 시작된 시간 즉 게임이 시작된 시간으로부터 경과한 시간을 초 단위로 나타냄(경과한 시간을 나타내므로 지속적으로 업데이트되는 FixedUpdate함수에 사용해야함)
        //아래 if문은 게임시작 2초후 gamemanager.대화(); 을 실행하는 함수
        //if (Time.time == 2)
        //{
        //    gamemanager.컷씬대화(400, false);
        //    Debug.Log("asda");
        //}

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

    //트리거와 충돌시 충돌 오브젝트 가져오기
    private void OnTriggerEnter2D(Collider2D collision)
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


