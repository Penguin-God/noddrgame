using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiecUI : MonoBehaviour
{ 
    public GameObject choiceObject;
    public GameObject[] choicePanel;
    public Text[] choiceText;
    public GameObject[] choiceCursor;

    public bool keyInput;
    private int count; // 배열의 크기
    private int result; // 선택한 선택창.

    public void Question()
    {
        count = choicePanel.Length - 1;
        choiceObject.SetActive(true);
        choiceCursor[0].SetActive(true);
        keyInput = true;
    }

    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                keyInput = false;
                ExitChoice();
            }
        }
    }

    public void Selection()
    {
        for(int i = 0; i < choicePanel.Length; i++)
        {
            choiceCursor[i].SetActive(false);
        }
        choiceCursor[result].SetActive(true);
    }

    void ExitChoice()
    {
        count = -1;
        choiceObject.SetActive(false);
        choiceCursor[0].SetActive(false);
        keyInput = false;
    }
}