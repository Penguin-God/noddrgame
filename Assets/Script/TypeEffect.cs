using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour // 대화창 텍스트 효과주는 script
{
    public Text Windowtext;

    public float TextSpeed;
    string TalkText;
    int TextIndex;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GetText(string text)
    {
        TalkText = text;
        EffectStart();
    }

    void EffectStart() 
    {
        Windowtext.text = ""; // Text창을 공백으로 만듬
        TextIndex = 0;

        Invoke("Effecting", 1 * TextSpeed); // 1 * TextSpeed : 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        if(TextIndex == TalkText.Length) // 대사를 다 출력하면 EffectEnd() 실행 후 return
        {
            //EffectEnd();
            return;
        }

        Windowtext.text += TalkText[TextIndex]; // 공백이 된 Text창에 TextIndex번째 문자열을 더함
        audioSource.Play();
        TextIndex++;

        Invoke("Effecting", 1 * TextSpeed); // 대사 다 출력할 때까지 재귀
    }

    void EffectEnd()
    {

    }
}
