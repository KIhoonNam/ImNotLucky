using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneScript : MonoBehaviour
{
   // public List<ItemData> PlayerItem = new List<ItemData>();
    public Transform slotRoot;
    
    public bool SellOn = false;
    public static System.Action<PlayerSprite.Rune> RuneClick;
    public PlayerSprite PlayerItem;
    
    public List<Slot> Rune;

    private void Awake()
    {
        PlayerItem = FindObjectOfType<PlayerSprite>();
    }
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        {
            PlayerItem.LoadPlayer();
        }
        Rune = new List<Slot>();


        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < PlayerItem.Stats.Slot + 1)
            {
                slot.RuneLoad(PlayerItem.RN[i]);
                Rune.Add(slot);

                Rune[i].enabled = false;
            }
           
        }
      
        InventoryScript.InvenClick += SetInven;
    }


    void SetInven(Item item)
    {
        //Debug.Log(item.name);

        var emptySlot = Rune.Find(t =>
        {
            return t.Player.name == null || t.Player.sprite == null;
        });

        if(emptySlot != null)
        {
            
            emptySlot.GetItem(item);
            SellOn = true;
        }
    }

    public void OnClick(Slot slot)
    {
        if (RuneClick != null)
        {

            RuneClick(slot.Player);
        }
    }
}
