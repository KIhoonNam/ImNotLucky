using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    Button button;
    Player Player;
    bool Stop;
    float[] CoolDown;
    SkilScript skill;
    StageManager SM;
    Image image;
    

    public static event SkillHandler Skill;
    private void Start()
    {
        
        Player = FindObjectOfType<Player>();
        image = GetComponent<Image>();
        CoolDown = new float[10];
        SM = FindObjectOfType<StageManager>();
        Set();
        skill = FindObjectOfType<SkilScript>();
        StageManager.SkillCool += SkillCoolDown;
    }


    public void OnClick()
    {
        
    }

    void Set()
    {
        for(int i = 0; i<CoolDown.Length; i++)
        {
            CoolDown[i] = 0;
        }
    }
    private void OnDestroy()
    {
        StageManager.SkillCool -= SkillCoolDown;
    }
    void SkillCoolDown()
    {
        Stop = false;
        for (int i = 0; i < this.CoolDown.Length; i++)
        {
            this.CoolDown[i]--;
        }
        if (this.image.sprite != null)
        {
            if (this.image.sprite.name == "RUNE_DEFEND+" && CoolDown[0] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_CRITICAL" && CoolDown[6] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_COUNTER" && CoolDown[5] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_ENCHANT" && CoolDown[7] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_HEAL" && CoolDown[4] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_IRON_SKIN" && CoolDown[2] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_NEUTRALIZE" && CoolDown[8] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_STRIKE" && CoolDown[1] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if (this.image.sprite.name == "RUNE_PROVOKE" && CoolDown[3] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
            else if(this.image.sprite.name == "RUNE_KILL" && CoolDown[9] <= 0)
            {
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
            }
        }
    }

    public void SkillOn()
    {
        if (!Stop && SM.State == StageState.PLAYERTURN && !SM.AttackTime)
        {
            if (this.image.sprite.name == "RUNE_DEFEND+" && CoolDown[0] <= 0)
            {
            
                Player.Index = 1;
                CoolDown[0] = 6;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_CRITICAL" && CoolDown[6] <= 0)
            {
                
                Player.Index = 7;
                CoolDown[6] = 5;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_COUNTER" && CoolDown[5] <= 0)
            {
           
                Player.Index = 6;
                CoolDown[5] = 4;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_ENCHANT" && CoolDown[7] <= 0)
            {
               
                Player.Index = 8;
                CoolDown[7] = 7;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_ENDURE")
            {
               
                Player.Index = 10;
                for (int i = 0; i < skill.Rune.Count; i++)
                {
                    if (this.image.sprite.name == skill.Rune[i].name)
                    {
                        Player.RN[i].name = null;
                      
                        this.image.enabled = false;
                    }
                }
                Setup();
            }
            else if (this.image.sprite.name == "RUNE_HEAL" && CoolDown[4] <= 0)
            {
               
                Player.Index = 5;
                CoolDown[4] = 5;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_IRON_SKIN" && CoolDown[2] <= 0)
            { 
                Player.Index = 3;
                CoolDown[2] = 7;
           
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_KILL"&&CoolDown[9] <= 0)
            {
              
                SM.AttackT();
                Player.Index = 11;
                CoolDown[9] = 99999;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);

            }
            else if (this.image.sprite.name == "RUNE_NEUTRALIZE" && CoolDown[8] <= 0)
            {
               
                Player.Index = 9;
                
                CoolDown[8] = 6;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_PROVOKE" && CoolDown[3] <= 0)
            {
               
                SM.AttackT();
                Player.Index = 4;
                CoolDown[3] = 3;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
            else if (this.image.sprite.name == "RUNE_RAGE")
            {
               
                Player.Index = 12;
                for (int i = 0; i < skill.Rune.Count; i++)
                {
                    if (this.image.sprite.name == skill.Rune[i].name)
                    {
                        Player.RN[i].name = null;
                        
                        this.image.enabled = false;
                    }
                }
                Setup();
            }
            else if (this.image.sprite.name == "RUNE_STRIKE" && CoolDown[1] <= 0)
            {
              
                SM.AttackT();
                Player.Index = 2;
                CoolDown[1] = 3;
                Setup();
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0.2f);
            }
              
        }

    }

    void Setup()
    {
        Skill();
        Stop = true;
        SM.State = StageState.ENEMYTURN;
    }

    
}
