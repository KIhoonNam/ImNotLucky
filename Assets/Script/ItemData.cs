using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData
{
    public string[] name;
    
    public ItemData(InventoryScript item)
    {
       
            name = new string[12];
      
        for (int i = 0; i< name.Length; i++)
        {
           
            if (name[i] == null)
            {
                name[i] = item.PlayerItem[i].name;
                
                
            }
        }
        
           

    }

}
[System.Serializable]
public class Item
{
    public string name;
    public Sprite spirte;
    public Item()
    {
        name = null;
        spirte = null;
    }
  
      


 

    
}


