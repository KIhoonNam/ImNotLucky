using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [HideInInspector]
    Itembuffer itembuffer;
    public InventoryScript Inven;
    [SerializeField]
    private Image NotG = null;
    GameManager gg;
    SoundScript sound;

    public static System.Action<ItemProperty> StoreClick;

    public Transform slotRoot;
    int[] Rand;

    PlayerSprite Player;
    private List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        gg = FindObjectOfType<GameManager>();
        sound = GetComponent<SoundScript>();
        itembuffer = FindObjectOfType<Itembuffer>();
        Player = FindObjectOfType<PlayerSprite>();
        Inven = FindObjectOfType<InventoryScript>();
        slots = new List<Slot>();
        Rand = new int[3];
        for(int i =0; i<3; i++)
        {
            if(gg.StageIndex == "Stage1")
            Rand[i] = Random.Range(0, 6);
            else if(gg.StageIndex == "Stage2")
                Rand[i] = Random.Range(0, 12);
            for (int j = 0; j < i; j++)
            {

                while (Rand[i] == Rand[j])
                {
                    if (gg.StageIndex == "Stage1")
                        Rand[i] = Random.Range(0, 6);
                    else if (gg.StageIndex == "Stage2")
                        Rand[i] = Random.Range(0, 12);
                }



            }
        }
        //if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        //{
        //    Player.LoadPlayer();

        //}
        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < itembuffer.items.Count)
            {
                slot.SetItem(itembuffer.items[Rand[i]]);
            }
            else
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            slots.Add(slot);
        }

        //if (System.IO.File.Exists(Application.persistentDataPath + " Item"))
        //{

        //    LoadItem();
        //    for(int i = 0; i<PlayerItem.Length; i++)
        //    {
        //        Debug.Log(PlayerItem[i].name);
        //    }
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < 3; i++)
            {
                if (gg.StageIndex == "Stage1")
                    Rand[i] = Random.Range(0, 12);
                else if (gg.StageIndex == "Stage2")
                    Rand[i] = Random.Range(0, 12);
                for (int j = 0; j < i; j++)
                {

                    while (Rand[i] == Rand[j])
                    {
                        if (gg.StageIndex == "Stage1")
                            Rand[i] = Random.Range(0, 12);
                        else if (gg.StageIndex == "Stage2")
                            Rand[i] = Random.Range(0, 12);
                    }



                }
            }
            //if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
            //{
            //    Player.LoadPlayer();

            //}
            int slotCnt = slotRoot.childCount;

            for (int i = 0; i < slotCnt; i++)
            {
                var slot = slotRoot.GetChild(i).GetComponent<Slot>();

                if (i < itembuffer.items.Count)
                {
                    slot.SetItem(itembuffer.items[Rand[i]]);
                }
                else
                    slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
                slots.Add(slot);
            }
        }
    }
    // Update is called once per frame
    public void OnSlotClick(Slot slot)
    {

        //       Click();


        if (Player.Stats.Gold >= slot.item.gold)
        {

            for (int i = 0; i < Inven.PlayerItem.Length; i++)
            {
                if (Inven.PlayerItem[i].name == slot.item.Name)
                {
                    break;
                }

                for (int j = 0; j < Player.RN.Length; j++)
                {
                    if (Player.RN[j].name == slot.item.Name)
                    {
                        goto stop;
                        
                    }
                }
                
                if (Inven.PlayerItem[i].name == "" || Inven.PlayerItem[i].name == null)
                {
                if (StoreClick != null)
                    StoreClick(slot.item);
                     Inven.PlayerItem[i].name = slot.item.Name;
                    sound.TRADE();
                    sound.Play();
                    Debug.Log(Inven.PlayerItem[i].name);
                    Player.Stats.Gold -= slot.item.gold;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    slot.button.interactable = false;
                    slot.ChildImage.color = new Color(slot.ChildImage.color.r, slot.ChildImage.color.g, slot.ChildImage.color.b, 0.6f);
                    slot.image.color = new Color(slot.ChildImage.color.r, slot.ChildImage.color.g, slot.ChildImage.color.b, 0.6f);
                    Inven.SaveItem();
                    Player.SavePlayer();
                    break;
                }

            }
        stop:;



        }
        else
        {
            if (NotG.enabled == false)
            {
                sound.NOTG();
                sound.Play();
                NotG.enabled = true;
                Invoke("NotGF", 1f);
            }
        }

    }


    void NotGF()
    {
        NotG.enabled = false;
    }




}
