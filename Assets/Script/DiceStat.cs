using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceStat : MonoBehaviour
{
    public static event ResultHandler Result;
    public static event DiceSteps DiceS;
    PlayerSprite Player;
    Di Dice;
    GameManager gg;
    [SerializeField]
    CanvasOnOff RollCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        gg = FindObjectOfType<GameManager>();
        Dice = FindObjectOfType<Di>();
        Player = FindObjectOfType<PlayerSprite>();
        DiceCheckZoneScript.DiceS += DiceChange;
        
    }


    void DiceChange()
    {
        Debug.Log("DiceChange");
        DiceCheckZoneScript.Stop = true;
        
        if (Dice.name == "BASIC_DICE(Clone)")
        {
            switch(DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    DiceS();
                    break;
                case 2:
                    DiceS();
                    break;
                case 3:
                    DiceS();
                    break;
                case 4:
                    DiceS();
                    break;
                case 5:
                    DiceS();
                    break;
                case 6:
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "FAST_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    DiceCheckZoneScript.diceNumber = 3;
                    DiceS();
                    break;
                case 2:
                    DiceCheckZoneScript.diceNumber = 3;
                    DiceS();
                    break;
                case 3:
                    DiceCheckZoneScript.diceNumber = 3;
                    DiceS();
                    break;
                case 4:
                    DiceCheckZoneScript.diceNumber = 4;
                    DiceS();
                    break;
                case 5:
                    DiceCheckZoneScript.diceNumber = 4;
                    DiceS();
                    break;
                case 6:
                    DiceCheckZoneScript.diceNumber = 5;
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "GOLD_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:

                    Player.Stats.Gold += 100;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(100, "Gold + ");
                    break;
                case 2:
                    Player.Stats.Gold += 150;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(150, "Gold + ");
                    break;
                case 3:
                    Player.Stats.Gold -= 50;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Result(50, "Gold - ");
                    Invoke("Set", 1.1f);
                    break;
                case 4:
                    DiceCheckZoneScript.diceNumber = 2;
                    DiceS();
                    break;
                case 5:
                    DiceCheckZoneScript.diceNumber = 3;
                    DiceS();
                    break;
                case 6:
                    DiceCheckZoneScript.diceNumber = 4;
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "GOLDEN_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    Player.Stats.Hp += 10;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(10, "HP + ");
                    break;
                case 2:
                    Player.Stats.Gold += 300;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(300, "Gold + ");
                    break;
                case 3:
                    Player.Stats.Damage += 5;
                    Result(5, "Damage + ");
                    Invoke("Set", 1.1f);
                    break;
                case 4:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
                case 5:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
                case 6:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "HALF_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    DiceCheckZoneScript.diceNumber = 1;
                    DiceS();
                    break;
                case 2:
                    DiceCheckZoneScript.diceNumber = 1;
                    DiceS();
                    break;
                case 3:
                    DiceCheckZoneScript.diceNumber = 1;
                    DiceS();
                    break;
                case 4:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
                case 5:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
                case 6:
                    DiceCheckZoneScript.diceNumber = 6;
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "HEAL_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    Player.Stats.Hp += 3;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(3, "HP + ");
                    break;
                case 2:
                    Player.Stats.Hp += 6;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set",1.1f);
                    Result(6, "HP + ");
                    break;
                case 3:
                    DiceCheckZoneScript.diceNumber = 0;
                    DiceS();
                    
                    break;
                case 4:
                    DiceCheckZoneScript.diceNumber = 1;
                    DiceS();
                    break;
                case 5:
                    DiceCheckZoneScript.diceNumber = 2;
                    DiceS();
                    break;
                case 6:
                    DiceCheckZoneScript.diceNumber = 3;
                    DiceS();
                    break;
            }
        }
        else if (Dice.name == "UNLUCKY_DICE(Clone)")
        {
            switch (DiceCheckZoneScript.diceNumber)
            {
                case 1:
                    Player.Stats.Hp += 1;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(1, "HP + ");
                    break;
                case 2:
                    Player.Stats.Hp += 10;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(10, "HP + ");
                    break;
                case 3:
                    Player.Stats.Hp += Player.Stats.MaxHp;
                    if (Player.Stats.Hp > Player.Stats.MaxHp)
                        Player.Stats.Hp = Player.Stats.MaxHp;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result((int)Player.Stats.MaxHp, "HP + ");
                    break;
                case 4:
                    Player.Stats.Gold += 1;
                    
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(1, "Gold + ");
                    break;
                case 5:
                    Player.Stats.Gold += 100;
                   
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(100, "Gold + ");
                    break;
                case 6:
                    Player.Stats.Gold += 9999;
                
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    Invoke("Set", 1.1f);
                    Result(9999, "Gold + ");
                    break;
            }
        }

    }
    private void OnDestroy()
    {
        DiceCheckZoneScript.DiceS -= DiceChange;
    }
    void Set()
    {
        gg.SaveGame();
        Player.SavePlayer();
        RollCanvas.On();
        gg.EndMoving = false;
        DiceCheckZoneScript.Stop = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
