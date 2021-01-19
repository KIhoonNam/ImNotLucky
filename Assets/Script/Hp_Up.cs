using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Up : MonoBehaviour
{
    [SerializeField]
    private Image NotG = null;

    PlayerSprite Player;

    SoundScript sound;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerSprite>();

        //if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        //{
        //    Player.LoadPlayer();

        //}
        sound = GetComponent<SoundScript>();
    }

    public void Hp_UP()
    {
        if (Player.Stats.Gold > 480 && Player.Stats.Hp != Player.Stats.MaxHp)
        {
            Player.Stats.Hp += Player.Stats.MaxHp * 0.25f;
            if (Player.Stats.Hp > Player.Stats.MaxHp)
                Player.Stats.Hp = Player.Stats.MaxHp;
            Player.Health.Initialize(Player.Stats.Hp, Player.Stats.MaxHp, Player.Stats.Gold);
            sound.HEAL();
            sound.Play();
            Player.SavePlayer();
        }
        else
        {
            if (Player.Stats.Hp != Player.Stats.MaxHp)
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
    }

    void NotGF()
    {
        NotG.enabled = false;
    }
}
