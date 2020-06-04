using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    static public play instance;
    public Gamemanager gamemanager;
    public float speed;
    public string currentmapname; //Scenechange script에 있는 mapname변수를 저장
    float h;
    float v;
    bool XMove;

    GameObject scan;
    GameObject crash;
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

        //ray 생성
        if (vDown && v == 1)
            dirvec = Vector3.up;
        if (vDown && v == -1)
            dirvec = Vector3.down;
        if (hDown && h == 1)
            dirvec = Vector3.right;
        if (hDown && h == -1)
            dirvec = Vector3.left;

        //object scan
        if (Input.GetButtonDown("Jump") && scan != null)
            gamemanager.Action(scan);

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
        RaycastHit2D rayhit = Physics2D.Raycast(Rigid.position, dirvec, 0.7f, LayerMask.GetMask("object"));

        if (rayhit.collider != null)
        {
            scan = rayhit.collider.gameObject;
        }
        else
            scan = null;
    }

    //트리거와 충돌시 충돌 오브젝트 가져오기
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != null && collision.gameObject.tag == "notmapchange")
        {
            //게임 오브젝트와 충돌했을 시 crash에 충돌한 게임오브젝트를 가져옴
            crash = collision.gameObject;
            gamemanager.이동금지영역(crash);
        }
        else
            crash = null;
    }
}


