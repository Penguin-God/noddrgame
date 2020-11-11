using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour // 대화창 텍스트 효과주는 script
{
    public Text Windowtext;
    public GameObject EndCursor;

    public float TextSpeed;
    public bool isTyping;

    string TalkText;
    int TextIndex;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FillText() // 현재 진행중인 대사를 다 보여주는 함수
    {
        CancelInvoke(); // 현재 실행되고있는 Invoke를 취소시킴
        Windowtext.text = TalkText; // 기존에 받은 talkText값을 text창에 넣음
        EffectEnd();
    }

    public void EffectStart(string text) // text = TalkManager에 있는 대사
    {
        TalkText = text;
        EndCursor.SetActive(false);
        Windowtext.text = ""; // Text창을 공백으로 만듬
        TextIndex = 0;
        isTyping = true;

        Invoke("Effecting", 1 * TextSpeed); // 1 * TextSpeed : 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        if(TextIndex == TalkText.Length) // 대사를 다 출력하면 EffectEnd() 실행 후 return
        {
            EffectEnd();
            return;
        }

        Windowtext.text += TalkText[TextIndex]; // EffectStart 함수에서 공백이 된 Text창에 TextIndex번째 문자열을 더함

        if(TalkText[TextIndex] != ' ' && TalkText[TextIndex] != '.') // ' ', '.'은 오디오 play 안함
            audioSource.Play();
            
        TextIndex++;
        Invoke("Effecting", 1 * TextSpeed); // 대사 다 출력할 때까지 재귀
    }

    void EffectEnd()
    {
        isTyping = false;
        EndCursor.SetActive(true);
    }
}
