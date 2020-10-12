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

    public Slider hpSilder;

    void Update()
    {
        hpSilder.maxValue = maxHp; 
        hpSilder.value = CurrentHp;
        PlayerDeat();
    }

    void PlayerDeat()
    {
        if (CurrentHp >= maxHp)
            StartCoroutine(HpSubtract());
    }

    IEnumerator HpSubtract()
    {
        playermanager.isaction = true;
        PlayerDie = true;
        while (CurrentHp > 0)
        {
            CurrentHp -= 3;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
