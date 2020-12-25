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
    bool hpAdding; // HP가 오르고 있을 때 true

    public Slider hpBar;
    public UIRotate UIrotate;

    void Update()
    {
        hpBar.maxValue = maxHp; 
        hpBar.value = CurrentHp;
        PlayerDeat();
        if (Input.GetButtonDown("Jump") && hpAdding) // hp 만땅 안되게 space눌러서 hp낮춤
            CurrentHp -= 5;
    }

    void PlayerDeat()
    {
        if (CurrentHp >= maxHp && !hpAdding)
            StartCoroutine(SubHp(5, 0.07f));
    }

    public IEnumerator AddHp(int AddHp, float WaitTime) 
    {
        playermanager.isaction = true;
        hpAdding = true;
        while (CurrentHp <= 50)
        {
            CurrentHp += AddHp;
            yield return new WaitForSeconds(WaitTime);
        }
        CurrentHp = 51;
        hpAdding = false;
    }

    IEnumerator SubHp(int SubHp, float WaitTime) 
    {
        UIrotate.ValueRotation(new Vector3(0, 0, 180)); // HpBar 180도 회전
        playermanager.isaction = true;
        while (CurrentHp > 0)
        {
            CurrentHp -= SubHp;
            yield return new WaitForSeconds(WaitTime);
        }
        CurrentHp = 0;
        PlayerDie = true;
    }
}