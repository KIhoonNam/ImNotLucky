using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnemyStats
{
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Damage { get; set; }
    public float Skill { get; set; }
    public float Gold { get; set; }
    public int Cool { get; set; }
    public int Rand { get; set; }

    public EnemyStats(float MaxHp,
    float Hp,
    float Damage,
    float Skill,
    float Gold,
    int Cool,
    int Rand
    )
    {
        this.MaxHp = MaxHp;
        this.Hp = Hp;
        this.Damage = Damage;
        this.Skill = Skill;
        this.Gold = Gold;
        this.Cool = Cool;
        this.Rand = Rand;
       
    }
}
public  class Enemy : MonoBehaviour
{
    public EnemyStats Stats;
    protected Transform Trans = null;
    protected SphereCollider AttackBox = null;
    protected StageManager Objects;
    protected Animator anim = null;
    protected GameObject icon;
    protected Vector3 ReTrans;
    protected Player player;
    protected Stats Health;
    protected GameObject DamageText;
    protected SoundScript Audio;
    protected string[] sound;
    protected GameObject Effects;
    public int Count = 0;

    // Start is called before the first frame update
    private void Start()
    {
        
    }
    protected void Damage(string name, GameObject transform)
    {
        Vector3 Position = new Vector3(transform.transform.position.x, transform.transform.position.y, transform.transform.position.z);
        var effect = Resources.Load<GameObject>(name);
        DamageText = Instantiate(effect, Position, Quaternion.identity);
        DamageText.transform.SetParent(GameObject.Find("Ef2").transform);
        DamageText.transform.position = Position;
        DamageText.transform.localScale = new Vector3(0.05f, 0.05f);


    }

    protected void PlayerSound()
    {
        if(player.Index == 7)
            player.Sound.PlayerVoice(player.Audio[5]);
        else if(player.anim.GetInteger("Index") == 11)
            player.Sound.PlayerVoice(player.Audio[10]);
        else if(player.Index == 2)
            player.Sound.PlayerVoice(player.Audio[14]);
        else
            player.Sound.PlayerVoice(player.Audio[0]);

    }
  
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void EnemySet(string name, Enemy enemy,string Hpname)
    {
        if (name == "Rhino_PBR")
        {

            enemy = GetComponent<Rhino_Enemy>();
            ((Rhino_Enemy)enemy).HealthSetUp(Hpname);
        }
        if (name == "Skeleton")
        {
            enemy = GetComponent<Skeleton_Enemy>();
            ((Skeleton_Enemy)enemy).HealthSetUp(Hpname);
        }
        if (name == "MushroomBlue")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).HealthSetUp(Hpname);
        }
        if (name == "MushroomRed")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).HealthSetUp(Hpname);
        }
        if (name == "MushroomGreen")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).HealthSetUp(Hpname);
        }
        if (name == "MiniGolem")
        {
            enemy = GetComponent<MiniGolem>();
            ((MiniGolem)enemy).HealthSetUp(Hpname);
        }
        if (name == "Spider")
        {
            enemy = GetComponent<Spider_Enemy>();
            ((Spider_Enemy)enemy).HealthSetUp(Hpname);
        }
        if (name == "RockGolem")
        {
            enemy = GetComponent<RockGolem>();
            ((RockGolem)enemy).HealthSetUp(Hpname);
        }
        if (name == "Grunt")
        {
            enemy = GetComponent<Goblin>();
            ((Goblin)enemy).HealthSetUp(Hpname);
        }
        if (name == "GruntBoss")
        {
            enemy = GetComponent<GoblinBoss>();
            ((GoblinBoss)enemy).HealthSetUp(Hpname);
        }
    }
    public void IconSet(string name, Enemy enemy)
    {
        if(icon != null)
        {
            Destroy(icon);
        }
        if (Count <= 0)
        {

            if (name == "Rhino_PBR")
            {

                enemy = GetComponent<Rhino_Enemy>();
                ((Rhino_Enemy)enemy).IconState();
            }
            if (name == "Skeleton")
            {
                enemy = GetComponent<Skeleton_Enemy>();
                ((Skeleton_Enemy)enemy).IconState();
            }
            if (name == "MushroomBlue")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).IconState();
            }
            if (name == "MushroomRed")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).IconState();
            }
            if (name == "MushroomGreen")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).IconState();
            }
            if (name == "MiniGolem")
            {
                enemy = GetComponent<MiniGolem>();
                ((MiniGolem)enemy).IconState();
            }
            if (name == "Spider")
            {
                enemy = GetComponent<Spider_Enemy>();
                ((Spider_Enemy)enemy).IconState();
            }
            if (name == "RockGolem")
            {
                enemy = GetComponent<RockGolem>();
                ((RockGolem)enemy).IconState();
            }
            if (name == "Grunt")
            {
                enemy = GetComponent<Goblin>();
                ((Goblin)enemy).IconState();
            }
            if (name == "GruntBoss")
            {
                enemy = GetComponent<GoblinBoss>();
                ((GoblinBoss)enemy).IconState();
            }
        }
    }
    public void ToDie(string name, Enemy enemy)
    {
        if (name == "Rhino_PBR")
        {

            enemy = GetComponent<Rhino_Enemy>();
            ((Rhino_Enemy)enemy).Die();
        }
        if (name == "Skeleton")
        {
            enemy = GetComponent<Skeleton_Enemy>();
            ((Skeleton_Enemy)enemy).Die();
        }
        if (name == "MushroomBlue")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Die();
        }
        if (name == "MushroomRed")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Die();
        }
        if (name == "MushroomGreen")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Die();
        }
        if (name == "MiniGolem")
        {
            enemy = GetComponent<MiniGolem>();
            ((MiniGolem)enemy).Die();
        }
        if (name == "Spider")
        {
            enemy = GetComponent<Spider_Enemy>();
            ((Spider_Enemy)enemy).Die();
        }
        if (name == "RockGolem")
        {
            enemy = GetComponent<RockGolem>();
            ((RockGolem)enemy).Die();
        }
        if (name == "Grunt")
        {
            enemy = GetComponent<Goblin>();
            ((Goblin)enemy).Die();
        }
        if (name == "GruntBoss")
        {
            enemy = GetComponent<GoblinBoss>();
            ((GoblinBoss)enemy).Die();
        }
    }
    public float EnemyGold()
    {
        return Stats.Gold;
    }
    public void Down(string name, Enemy enemy)
    {
        if (name == "Rhino_PBR")
        {

            enemy = GetComponent<Rhino_Enemy>();
            ((Rhino_Enemy)enemy).Do();
        }
        if (name == "Skeleton")
        {
            enemy = GetComponent<Skeleton_Enemy>();
            ((Skeleton_Enemy)enemy).Do();
        }
        if (name == "MushroomBlue")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Do();
        }
        if (name == "MushroomRed")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Do();
        }
        if (name == "MushroomGreen")
        {
            enemy = GetComponent<Mushroom>();
            ((Mushroom)enemy).Do();
        }
        if (name == "MiniGolem")
        {
            enemy = GetComponent<MiniGolem>();
            ((MiniGolem)enemy).Do();
        }
        if (name == "Spider")
        {
            enemy = GetComponent<Spider_Enemy>();
            ((Spider_Enemy)enemy).Do();
        }
        if (name == "RockGolem")
        {
            enemy = GetComponent<RockGolem>();
            ((RockGolem)enemy).Do();
        }
        if (name == "Grunt")
        {
            enemy = GetComponent<Goblin>();
            ((Goblin)enemy).Do();
        }
        if (name == "GruntBoss")
        {
            enemy = GetComponent<GoblinBoss>();
            ((GoblinBoss)enemy).Do();
        }
    }

    public void Go(string name,Enemy enemy)
    {
        if (icon != null)
        {
            Destroy(icon);
        }
        if (Count <= 0)
        {
            if (name == "Rhino_PBR")
            {

                enemy = GetComponent<Rhino_Enemy>();
                ((Rhino_Enemy)enemy).State();
            }
            if (name == "Skeleton")
            {
                enemy = GetComponent<Skeleton_Enemy>();
                ((Skeleton_Enemy)enemy).State();
            }
            if (name == "MushroomBlue")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).State();
            }
            if (name == "MushroomRed")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).State();
            }
            if (name == "MushroomGreen")
            {
                enemy = GetComponent<Mushroom>();
                ((Mushroom)enemy).State();
            }
            if (name == "MiniGolem")
            {
                enemy = GetComponent<MiniGolem>();
                ((MiniGolem)enemy).State();
            }
            if(name == "Spider")
            {
                enemy = GetComponent<Spider_Enemy>();
                ((Spider_Enemy)enemy).State();
            }
            if (name == "RockGolem")
            {
                enemy = GetComponent<RockGolem>();
                ((RockGolem)enemy).State();
            }
            if (name == "Grunt")
            {
                enemy = GetComponent<Goblin>();
                ((Goblin)enemy).State();
            }
            if (name == "GruntBoss")
            {
                enemy = GetComponent<GoblinBoss>();
                ((GoblinBoss)enemy).State();
            }
        }
        else
        {
            Count--;
            Objects.Stop++;
            StartCoroutine(Objects.EnemyAttack());
        }
        
    }

   
    
    

}
