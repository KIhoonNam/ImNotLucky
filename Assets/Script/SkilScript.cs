using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilScript : MonoBehaviour
{
    public Transform slotRoot;

    public PlayerSprite PlayerItem;

    public List<Slot> Rune;
    private void Awake()
    {
        PlayerItem = FindObjectOfType<PlayerSprite>();
    }

    void Start()
    {
        Rune = new List<Slot>();


        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < PlayerItem.Stats.Slot + 1)
            {
                slot.SkillLoad(PlayerItem.RN[i]);
                Rune.Add(slot);

                Rune[i].enabled = false;
            }

        }
    }
}
