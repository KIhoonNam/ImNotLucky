using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    public static event DiceChange DiceS;
    Vector3 diceVelocity;
    public static int diceNumber;
    public static bool Stop;
    bool input;
    GameManager gg;
    // Update is called once per frame
    

    void Start()
    {
        Stop = false;

        gg = FindObjectOfType<GameManager>();
    }
    void FixedUpdate()
    {
        diceVelocity = Di.DiceVelocity;
       
    }

 
     

    private void OnTriggerStay(Collider other)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            if (!Stop)
            {
                switch (other.gameObject.name)
                {
                    case "Side1":
                        diceNumber = 6;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                    case "Side2":
                        diceNumber = 5;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                    case "Side3":
                        diceNumber = 4;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                    case "Side4":
                        diceNumber =3;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                    case "Side5":
                        diceNumber = 2;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                    case "Side6":
                        diceNumber = 1;
                        if (DiceS != null && gg.EndMoving == true)
                        {
                            DiceS();
                        }
                        break;
                }
                
            }
        }
    }


}
