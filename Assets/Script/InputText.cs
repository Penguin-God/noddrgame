using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public Text nickname;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(nickname.text);
            Destroy(this.gameObject);
        }
    }
}
