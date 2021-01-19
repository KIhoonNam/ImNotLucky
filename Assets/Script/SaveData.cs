using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public float MyHP { get; set; }
    public float MyMaxHP { get; set; }

    public float Damage { get; set; }

    public float DiceIndex { get; set; }
    public float Armor { get; set; }
    public float Gold { get; set; }
    public float Slot { get; set; }
    public string[] Runename { get; set; }
    public string[] Dicename { get; set; }
    
 
    public SaveData(PlayerSprite player)
    {
        Runename = new string[6];
        Dicename = new string[8];

        MyHP = player.Stats.Hp;
        MyMaxHP = player.Stats.MaxHp;
        Damage = player.Stats.Damage;
        Armor = player.Stats.Armor;
        Gold = player.Stats.Gold;
        Slot = player.Stats.Slot;
        DiceIndex = player.Stats.DiceIndex;
        for(int i = 0; i<Runename.Length; i++)
        {
            if(Runename[i] == null)
            {
                Runename[i] = player.RN[i].name;
            }
        }
        for (int i = 1; i<Dicename.Length;i++)
        {
            if(Dicename[i] == null)
            {
                Dicename[i] = player.DN[i].name;
            }
        }

        
    }

 
}



