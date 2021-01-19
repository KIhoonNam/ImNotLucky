using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Itembuffer : MonoBehaviour
{
    public List<ItemProperty> items;

    public List<ItemProperty> Dice;
    
    private void Start()
    {
        Dice[5].gold = Random.Range(800, 1501);
        


      
        
    }
 

    
}
