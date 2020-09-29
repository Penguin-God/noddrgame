using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat playerStat;

    public int maxHp;
    public int HP;
    public int maxMp;
    public int MP;

    public Slider hpSilder;

    void Start()
    {
        playerStat = this;
    }

    void Update()
    {
        hpSilder.maxValue = maxHp;
        hpSilder.value = HP;
    }
}
