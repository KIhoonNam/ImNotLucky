using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public struct PlayerStats
    {

        public float MaxHp { get; set; }

        public float Hp { get; set; }

        public float Damage { get; set; }

        public float Armor { get; set; }
        public float DamagePlus { get; set; }
       
        public float Slot { get; set; }

        public float DiceIndex { get; set; }
        public float Gold { get; set; }
        public float ArmorPlus { get; set; }
        public float Enchant { get; set; }
        public float Endure { get; set; }
        public PlayerStats(float DiceIndex,float Enchant,float Endure,float MaxHp, float Hp, float Damage, float Armor, float Gold,float Slot,float DPlus,float APlus)
        {
            this.Hp = Hp;
            this.MaxHp = MaxHp;
            this.Damage = Damage;
            this.Armor = Armor;
            this.Slot = Slot;
            this.DamagePlus = DPlus;
            this.Gold = Gold;
            this.ArmorPlus = APlus;
            this.Endure = Endure;
            this.Enchant = Enchant;
            this.DiceIndex = DiceIndex;

        }
    }
    public struct Rune
    {
        public string name;

        public Sprite sprite;

        public Rune(string name,Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }

    public struct Dice
    {
        public string name;

        public Sprite sprite;

        public Dice(string name,Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }
    
    [SerializeField]
    public Stats Health;
    public PlayerStats Stats;
    public Rune[] RN = new Rune[6];
    public Dice[] DN = new Dice[8];
   

    
    protected virtual void OnEnable()
    {
        Stats.Hp = 70;
        Stats.MaxHp = 70;
        Stats.Gold = 9999;
        Stats.Armor = 2;
        Stats.Damage = 100;
        Stats.Slot = 0;
        Stats.ArmorPlus = 0;
        Stats.DamagePlus = 0;
        Stats.Endure = 0;
        Stats.Enchant = 1;
        Stats.DiceIndex = 1;
        DN[0].name = "BASIC_DICE";
        DN[0].sprite = Resources.Load<Sprite>("DiceChose/" + DN[0].name) as Sprite;

        

    }
    protected virtual void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        {

            LoadPlayer();
        }

        if(GameObject.Find("Health"))
         Health = GameObject.Find("Health").GetComponent<Stats>();
        if (Health != null)
            Health.Initialize(Stats.Hp, Stats.MaxHp, Stats.Gold);


    }

    protected virtual void Update()
    {
        

       
    }

 
    public void SavePlayer()
    {
        SaveManager.Save(this);
    }

    public void LoadPlayer()
    {
        SaveData data = SaveManager.Load();

        Stats.Hp = data.MyHP;
        Stats.MaxHp = data.MyMaxHP;
        Stats.Gold = data.Gold;
        Stats.Armor = data.Armor;
        Stats.Damage = data.Damage;
        Stats.Slot = data.Slot;
        Stats.DiceIndex = data.DiceIndex;
        for(int i = 0; i<RN.Length; i++)
        {
            RN[i].name = data.Runename[i];
            RN[i].sprite = Resources.Load<Sprite>(RN[i].name) as Sprite;
        }
        for (int i = 1; i < DN.Length; i++)
        {
            DN[i].name = data.Dicename[i];
            DN[i].sprite = Resources.Load<Sprite>("DiceChose/" + DN[i].name) as Sprite;
        }

    }
}
