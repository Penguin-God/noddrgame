using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStat : MonoBehaviour
{
    public Playermanager playermanager;

    public int maxHp;
    public int CurrentHp;

    public bool PlayerDie;
    bool addHp; // HP가 오르고 있을 때 true

    public Slider hpBar;
    public UIRotate UIrotate;

    void Update()
    {
        hpBar.maxValue = maxHp; 
        hpBar.value = CurrentHp;
        PlayerDeat();
        if (Input.GetButtonDown("Jump") && addHp) // hp 만땅 안되게 space눌러서 hp낮춤
            CurrentHp += 5;
    }

    void PlayerDeat()
    {
        if (CurrentHp >= maxHp)
            StartCoroutine(HpSubtract());
    }

    IEnumerator HpSubtract()
    {
        addHp = true;
        UIrotate.ValueRotation(new Vector3(0, 0, 180)); // HpBar 180도 회전
        PlayerDie = true;
        playermanager.isaction = true;
        while (CurrentHp > 0)
        {
            CurrentHp -= 5;
            yield return new WaitForSeconds(0.5f);
        }
        CurrentHp = 0;
        addHp = false;
    }
}