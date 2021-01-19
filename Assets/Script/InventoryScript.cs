using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [HideInInspector]
    public Itembuffer itembuffer;

    public Item[] PlayerItem = new Item[12];


    public Transform slotRoot;
    public static System.Action<Item>  InvenClick;

    public bool SellOn = false;
    [SerializeField]
    public List<Slot> Rune;
    private void Awake()
    {
       
    }
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + " Item"))
            LoadItem();
        Rune = new List<Slot>();

        if (slotRoot != null)
        {
            int slotCnt = slotRoot.childCount;

            for (int i = 0; i < slotCnt; i++)
            {
                var slot = slotRoot.GetChild(i).GetComponent<Slot>();
                if (i < PlayerItem.Length)
                {
                    slot.InvenLoad(PlayerItem[i]);
                }



                Rune.Add(slot);



            }
            RuneScript.RuneClick += SetRune;
            Store.StoreClick += SetStore;
        }
    }
    public void SaveItem()
    {
        SaveManager.ItemSave(this);
    }
    public void LoadItem()
    {
        ItemData data = SaveManager.ItemLoad();

        for (int i = 0; i < PlayerItem.Length; i++)
        {
            PlayerItem[i].name = data.name[i];
            PlayerItem[i].spirte = Resources.Load<Sprite>(PlayerItem[i].name) as Sprite;
        }
        

    }

    public void OnClick(Slot slot)
    {
        
        if(InvenClick != null)
        {

            InvenClick(slot.LItem);
        }
       
    }
    void SetRune(PlayerSprite.Rune item)
    {
        var emptySlot = Rune.Find(t =>
        {
            return t.LItem == null || t.LItem.name == string.Empty;
        });
        if (emptySlot != null)
        {
            emptySlot.PlayerItem(item);
            SellOn = true;
        }
    }

    void SetStore(ItemProperty item)
    {
        var emptySlot = Rune.Find(t =>
        {
            return t.LItem == null || t.LItem.name == string.Empty;
        });
        if (emptySlot != null)
        {
            emptySlot.MoveItem(item);
            
        }
    }
}
