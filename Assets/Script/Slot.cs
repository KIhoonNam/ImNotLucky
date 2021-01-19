using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
     InventoryScript Inven;
     RuneScript Rune;
    [HideInInspector]
    public Image ChildImage;
    public Item LItem;
    public Image image;
    [HideInInspector]
    public PlayerSprite.Rune Player;

    [HideInInspector]

    public PlayerSprite.Dice DiPlayer;
    
    PlayerSprite SavePlayer;
    [HideInInspector]
    public Button button;

    Slot slot;
    
    private void OnEnable()
    {
        slot = GetComponent<Slot>();
        Rune = FindObjectOfType<RuneScript>();
        Inven = FindObjectOfType<InventoryScript>();
        ChildImage = GetComponentInChildren<Image>();
        button = GetComponent<Button>();
        SavePlayer = FindObjectOfType<PlayerSprite>();
    }


    private void Start()
    {
        slot.enabled = true;
    }

    public void SetDice(ItemProperty item)
    {
        this.item = item;

        if (item == null)
        {
            image.enabled = false;

            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.Name;
            image.sprite = item.sprite;

        }
    }
    public void SetItem(ItemProperty item)
    {
        this.item = item;
        
        if(item == null )
        {
            image.enabled = false;

            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;
            
            gameObject.name = item.Name;
            image.sprite = item.sprite;
            
        }
    }
    public void MoveItem(ItemProperty item)
    {
        this.item = item;

        if (item == null)
        {
            image.enabled = false;

            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.Name;
            image.sprite = Resources.Load<Sprite>(gameObject.name) as Sprite;

        }
    }
    public void MoveDice(ItemProperty item)
    {
        this.item = item;

        if (item == null)
        {
            image.enabled = false;

            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.Name;
            image.sprite = Resources.Load<Sprite>("DiceChose/" + gameObject.name) as Sprite;
            this.DiPlayer.name = item.Name;
            this.DiPlayer.sprite = item.sprite;
        }
    }
    public void InvenLoad(Item item)
    {
        this.LItem = item;

        if (item == null || item.spirte == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {


            image.enabled = true;
            gameObject.name = item.spirte.name;
            image.sprite = item.spirte;


        }
    }
    public void RuneLoad(PlayerSprite.Rune item)
    {
        this.Player = item;

        if (item.name == null || item.sprite == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {

            image.enabled = true;
            gameObject.name = item.sprite.name;
            image.sprite = item.sprite;



        }
    }
    public void DiceLoad(PlayerSprite.Dice item)
    {
        this.DiPlayer = item;

        if (item.name == null || item.sprite == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {

            image.enabled = true;
            gameObject.name = item.sprite.name;
            image.sprite = item.sprite;



        }
    }
    public void GetItem(Item item)
    {
       

        if (item == null || item.spirte == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {


            image.enabled = true;
            gameObject.name = item.spirte.name;
            image.sprite = item.spirte;
            this.Player.name = item.spirte.name;
            this.Player.sprite = item.spirte;


        }
    }
    public void PlayerItem(PlayerSprite.Rune item)
    {
       

        if (item.name == null || item.sprite == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {
            
            image.enabled = true;
            gameObject.name = item.sprite.name;
            image.sprite = item.sprite;
            this.LItem.name = item.sprite.name;
            this.LItem.spirte = item.sprite;



        }
    }
    
    public void SkillLoad(PlayerSprite.Rune item)
    {
        this.Player = item;

        if (item.name == null || item.sprite == null)
        {
            image.enabled = false;
            gameObject.name = "";
        }
        else
        {

            image.enabled = true;
            gameObject.name = item.sprite.name;
            image.sprite = Resources.Load<Sprite>("Skill/" + item.sprite.name) as Sprite;



        }
    }

   public void OnSell()
    {
        if (Rune != null)
        {
            if (Rune.SellOn)
            {
                Rune.SellOn = false;
                GetItem(null);
                for (int i = 0; i < Inven.PlayerItem.Length; i++)
                {
                    Inven.PlayerItem[i].name = Inven.Rune[i].name;
                }
                Inven.SaveItem();
                for (int i = 0; i < Rune.Rune.Count; i++)
                {
                    
                       SavePlayer.RN[i].name = Rune.Rune[i].name;
                   
                }
                SavePlayer.SavePlayer();
                
            }
        }
    }

    public void OnUnE()
    {
        if (Inven != null)
        {
            if (Inven.SellOn)
            { 
                Inven.SellOn = true;
                this.Player.name = null;
                this.Player.sprite = null;
                GetItem(null);
                for (int i = 0; i < Inven.PlayerItem.Length; i++)
                {
                    Inven.PlayerItem[i].name = Inven.Rune[i].name;
                }
                Inven.SaveItem();
                for (int i = 0; i < Rune.Rune.Count; i++)
                {

                    SavePlayer.RN[i].name = Rune.Rune[i].name;
                }
                SavePlayer.SavePlayer();
               
            }
        }
    }
}
