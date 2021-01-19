using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string[] Name { get; set; }

    public Sprite[] sprite { get; set; }

    public Inventory(string[] _name, Sprite[] _sprite)
    {
        Name = _name;
        sprite = _sprite;
    }

    public List<ItemData> PlayerItem = new List<ItemData>();
    public Transform slotRoot;
    
    private List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Slot>();


        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

         
            slots.Add(slot);
        }
        
    }

    
}
