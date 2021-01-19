using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSlotScript : MonoBehaviour
{
    [SerializeField]
    private Image NotG = null;
    Image image;
    PlayerSprite Player;
    SoundScript sound;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerSprite>();
        image = GetComponent<Image>();
        sound = GetComponent<SoundScript>();
        //if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        //{
        //    Player.LoadPlayer();
        //}
        if (Player.Stats.Slot != 0)
        {
            switch (Player.Stats.Slot)
            {
                case 1:
                    image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
                    break;
                case 2:
                    image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
                    break;
                case 3:
                    image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
                    break;
                case 4:
                    image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
                    break;
                case 5:
                    image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
                    break;
            }
        }
    }


    public void RuneSlotSell()
    {
        if(Player.Stats.Slot <= 4 && Player.Stats.Gold >= 100)
        {
            Player.Stats.Gold -= 100;
            Player.Stats.Slot++;
            Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
            Player.SavePlayer();
            image.sprite = Resources.Load<Sprite>("OPEN_" + Player.Stats.Slot);
            sound.RuneSocket();
            sound.Play();
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
