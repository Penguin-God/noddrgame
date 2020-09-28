using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat playerStat;

    public int MaxHp;
    public int CurrentHp;
    public int MaxMp;
    public int CurrentMp;

    void Start()
    {
        playerStat = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
