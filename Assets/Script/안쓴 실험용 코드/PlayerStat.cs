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

    public Slider hpBar;
    public UiMove UiMove;

    void Update()
    {
        hpBar.maxValue = maxHp; 
        hpBar.value = CurrentHp;
        PlayerDeat();
    }

    void PlayerDeat()
    {
        if (CurrentHp >= maxHp)
            StartCoroutine(HpSubtract());
    }

    IEnumerator HpSubtract()
    {
        UiMove.ValueRotation(new Vector3(0, 0, 180));
        PlayerDie = true;
        playermanager.isaction = true;
        while (CurrentHp > 0)
        {
            CurrentHp -= 5;
            yield return new WaitForSeconds(0.05f);
        }
        CurrentHp = 0;
    }
}
