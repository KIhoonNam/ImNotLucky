using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIInvenScript : MonoBehaviour
{


    public PlayerSprite PlayerDice;
    public Transform slotRoot;

    private List<Slot> slots;

    private void Awake()
    {
        PlayerDice = FindObjectOfType<PlayerSprite>();
    }
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        {
            PlayerDice.LoadPlayer();
        }
        slots = new List<Slot>();


        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < PlayerDice.DN.Length)
            {
                slot.DiceLoad(PlayerDice.DN[i]);
                slots.Add(slot);

                slots[i].enabled = false;
            }

        }
       // PlayerDice.Health.Initialize(PlayerDice.Stats.Hp, PlayerDice.Stats.MaxHp, PlayerDice.Stats.Gold);
        DiceStore.StoreClick += SetStore;
    }

    void SetStore(ItemProperty item)
    {
        var emptySlot = slots.Find(t =>
        {
            return t.DiPlayer.name == null || t.DiPlayer.sprite == null ;
        });
        if (emptySlot != null)
        {
            emptySlot.MoveDice(item);

        }
    }
}
