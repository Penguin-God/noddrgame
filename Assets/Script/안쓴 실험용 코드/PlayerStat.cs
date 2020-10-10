using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStat : MonoBehaviour
{
    public int maxHp;
    public int CurrentHp;

    public bool PlayerSurvival = true;

    public Slider hpSilder;

    void Update()
    {
        hpSilder.maxValue = maxHp; 
        hpSilder.value = CurrentHp;
        PlayerDie();
    }

    void PlayerDie()
    {
        if (CurrentHp >= maxHp)
            StartCoroutine(HpSubtract());
    }

    IEnumerator HpSubtract()
    {
        PlayerSurvival = false;
        while (CurrentHp > 0)
        {
            CurrentHp -= 3;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
