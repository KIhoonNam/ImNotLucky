using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceStore : MonoBehaviour
{
    [HideInInspector]
    Itembuffer item;
    [SerializeField]
    private Image NotG = null;

    public DIInvenScript DiInven;

    SoundScript sound;

    public static System.Action<ItemProperty> StoreClick;
    public Transform slotRoot;
    int[] Rand;
   

    PlayerSprite Player;
    private List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        DiInven = FindObjectOfType<DIInvenScript>();
        sound = GetComponent<SoundScript>();
        item = FindObjectOfType<Itembuffer>();
        Player = FindObjectOfType<PlayerSprite>();
        slots = new List<Slot>();
        Rand = new int[3];

        for (int i = 0; i < 3; i++)
        {
            Rand[i] = Random.Range(0, item.Dice.Count);
            for (int j = 0; j < i; j++)
            {

                while (Rand[i] == Rand[j])
                {
                    Rand[i] = Random.Range(0, item.Dice.Count);
                    j = 0;
                }


            }
        }

        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < item.Dice.Count)
            {
                slot.SetItem(item.Dice[Rand[i]]);
            }
            else
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            slots.Add(slot);
        }
    }
    public void OnSlotClick(Slot slot)
    {
        if (Player.Stats.Gold >= slot.item.gold)
        {

            for (int i = 0; i < Player.DN.Length; i++)
            {
                if (Player.DN[i].name == slot.item.Name)
                {
                    break;
                }

                if (i < Player.DN.Length)
                {
                    if (Player.DN[i].name != null && Player.DN[i].name == slot.item.Name)
                        break;
                }
                if (Player.DN[i].name == "" || Player.DN[i].name == null)
                {
                    if (StoreClick != null)
                        StoreClick(slot.item);
                    Player.DN[i].name = slot.item.Name;
                    DiInven.PlayerDice.name = slot.item.Name;
                    sound.TRADE();
                    sound.Play();
                    Debug.Log(Player.DN[i].name);
                    Player.Stats.Gold -= slot.item.gold;
                    Player.Stats.DiceIndex++;
                    Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
                    slot.button.interactable = false;
                    slot.ChildImage.color = new Color(slot.ChildImage.color.r, slot.ChildImage.color.g, slot.ChildImage.color.b, 0.6f);
                    slot.image.color = new Color(slot.ChildImage.color.r, slot.ChildImage.color.g, slot.ChildImage.color.b, 0.6f);
                    
                    Player.SavePlayer();
                    break;
                }

            }
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
    // Update is called once per frame
   
}
