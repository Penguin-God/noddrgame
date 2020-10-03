using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStat : MonoBehaviour
{
    public int maxHp;
    public int CurrentHp;
   
    public Slider hpSilder;

    void Update()
    {
        hpSilder.maxValue = maxHp; 
        hpSilder.value = CurrentHp;
        //if (CurrentHp > maxHp)
        //    Destroy(gameObject, 1);
    }
}
