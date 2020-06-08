using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour 대신에 상속받고 싶은 script의 이름을 쓰면 상속이 됨
public class Playermanager : play //play한테 여러 변수를 상속받고 있음
{
    public string CharacterName;

    GameObject TalkObject;
    
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

        //ray 생성
        if (vDown && v == 1)
            dirvec = Vector3.up;
        if (vDown && v == -1)
            dirvec = Vector3.down;
        if (hDown && h == 1)
            dirvec = Vector3.right;
        if (hDown && h == -1)
            dirvec = Vector3.left;

        if (Input.GetButtonDown("Jump") && TalkObject != null)
            gamemanager.Action(TalkObject);
        //에러난 코드(첫번째 충돌 후 다시 가면 대사가 안뜨고 다시 충돌해야 대사가 뜸)
        //else if (gamemanager.isaction = true && Input.GetButtonDown("Jump"))
        //{
        //    gamemanager.isaction = false;
        //    gamemanager.talkwindow.SetActive(gamemanager.isaction);
        //    Debug.Log("asfg");
        //}
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = XMove ? new Vector2(h, 0) : new Vector2(0, v);
        Rigid.velocity = moveVec * speed;

        Debug.DrawRay(Rigid.position, dirvec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(Rigid.position, dirvec, 0.7f, LayerMask.GetMask("Object"));

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
            gamemanager.Action(TalkObject);
        }
        else
            TalkObject = null;
    }
}


