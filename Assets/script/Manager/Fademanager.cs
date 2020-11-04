﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fademanager : MonoBehaviour
{
    public SpriteRenderer WhiteWindow;
    public SpriteRenderer BlackWIndow;
    public Image Blackimg;
    public Color color;

    private WaitForSeconds FadeoutWaitTime = new WaitForSeconds(0.01f);
    //while문 안에 new연산자를 쓰면 메모리 손해이기 때문에 위쪽에 생성(원래 코루틴 안에 new를 쓰기도 함)

    public void FadeOut(float Speed = 0.02f) //아무값도 넣지 않으면 Speed는 0.02f임
    {
        StopAllCoroutines();//코루틴이 겹치면서 혼선이 일어날 수 있으니 함수 시작 시 전에 돌아가던 모든 코루틴을 멈춤
        StartCoroutine(FadeOutCoroutine(Speed));
    }

    IEnumerator FadeOutCoroutine(float Speed)
    {
        color = BlackWIndow.color;
        while(color.a < 1f)// a = 알파(투명도)값
        {
            color.a += Speed;
            BlackWIndow.color = color; 
            yield return FadeoutWaitTime; //알파값에 Speed를 계속 더하고 그값을 BlackWIndow에 준 후 0.01f만큼의 대기시간을 가짐(약 1초동안 이 과정이 이루어짐)
        }
    }

    //위쪽의 코드를 거의 그대로 갖고옴
    public void FadeIn(float Speed = 0.02f) //아무값도 넣지 않으면 Speed는 0.02f임
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(Speed));
    }

    IEnumerator FadeInCoroutine(float Speed)
    {
        color = BlackWIndow.color;
        while (color.a > 0f)//a = 0이 되면 검은색화면이 없어졌다는 의미이므로 while문 정지
        {
            color.a -= Speed;
            BlackWIndow.color = color;
            yield return FadeoutWaitTime; 
        }
    }

    public void UIFadeIn(float Speed = 0.02f)
    {
        StartCoroutine(UIFadeinCo(Speed));
    }

    IEnumerator UIFadeinCo(float Speed)
    {
        color = Blackimg.color;
        while (color.a > 0f) // 검은창 천천히 투명하게 하는 코드
        {
            color.a -= Speed;
            Blackimg.color = color;
            yield return FadeoutWaitTime;
        }
    }
}
