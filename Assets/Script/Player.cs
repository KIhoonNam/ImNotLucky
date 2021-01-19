using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public delegate void TakeDmageHandler();

public class Player : PlayerSprite
{

    public int Index;
    public static event TakeDmageHandler TakeDamage;
    Transform Trans = null;
    public SphereCollider AttackBox = null;
    public StageManager Objects;
    public Animator anim = null;
    int Enchant = 0;
    public PlayerStats Stat;
    private static Player instance;
    public Enemy enemy;
    GameObject Effects;
    public SoundScript Sound;
    public string[] Audio;
    public string[] SkillAudio;
    Text ArmorText = null;
    Text AttacText = null;
    Vector3 ReTrans;

    public static PlayerSprite MyInstance
    {

        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public void SetText()
    {

        ArmorText = GameObject.Find("Armor").transform.GetChild(0).GetComponent<Text>();
        ArmorText.text = (Stats.Armor +  Stats.ArmorPlus) + "";
        AttacText = GameObject.Find("Damage").transform.GetChild(0).GetComponent<Text>();
        AttacText.text = ((Stats.Damage + Stats.DamagePlus)*Stats.Enchant) + "";
    }
    public void Effect(string name,GameObject transform,Vector3 Sclae,float Rotate)
    {
        Vector3 Position = new Vector3(transform.transform.position.x, transform.transform.position.y, transform.transform.position.z);
        var effect = Resources.Load<GameObject>("Effect/"+name);
        Effects = Instantiate(effect,Position,Quaternion.identity);
        Effects.transform.SetParent(GameObject.Find("Ef2").transform);
        Effects.transform.position = Position;
        Effects.transform.localScale = Sclae;
        if (Rotate != 0)
        {
            Effects.transform.rotation = Quaternion.Euler(0, 0, Rotate);
        }
        if(anim.GetInteger("Index") == 12)
            Effects.transform.rotation = Quaternion.Euler(75, 0, 0);

    }
    protected override  void OnEnable()
    {

        Sound = GetComponent<SoundScript>();

        if (Trans == null)
            Trans = GetComponent<Transform>();
        if (anim == null)
            anim = GetComponent<Animator>();
        if (AttackBox == null)
            AttackBox = GetComponentInChildren<SphereCollider>();
        enemy = GetComponent<Enemy>();



        Objects = GameObject.Find("StateSystem").GetComponent<StageManager>();



        base.OnEnable();
    }

    protected override void Start()
    {
        Audio = new string[] { "Attack", "DEATH", "COUNTER_1","COUNTER_2","DEFEND+","CRITICAL",
        "ENCHANT","ENDURE","HEAL","IRON_SKIN","KILL","NEUTRALIZE","PROVOKE","RAGE","STRIKE"};
        SkillSlot.Skill += Play;

        ReTrans = Trans.position;
       
        base.Start();


        
    }
    protected override void Update()
    {



        if (anim.GetBool("IsAttack"))
        {
            
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75f)
                AttackBox.enabled = false;
            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.25f)
            {
                AttackBox.enabled = true;
            }
        }


        base.Update();
    }


    public void Cool()
    {
        if (Enchant < 0)
            Stats.Enchant = 1f;
        Enchant--;
    }
    public void Play()
    {
        switch (Index)
        {
            case 1:
                Stats.ArmorPlus += 2;
                SetText();
                Sound.PlayerVoice(Audio[4]);
                Sound.Play();
                Effect("Skill02_Start", this.transform.GetChild(2).gameObject, new Vector3(0.3f,0.3f,0), 0f);
                anim.SetInteger("Index", 1);
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 2:

                Stats.DamagePlus += 30f - Stats.Damage;
                SetText();
                anim.SetInteger("Index", 2);
                break;
            case 3:
                Sound.PlayerVoice(Audio[9]);
                Sound.Play();
                Stats.ArmorPlus += 1000;
                SetText();
                anim.SetInteger("Index", 3);
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 4:
              
                break;
            case 5:
                Sound.PlayerVoice(Audio[8]);
                Sound.Play();
                anim.SetInteger("Index", 5);
                Stats.Hp += 13;
                if (Stats.Hp > Stats.MaxHp)
                {
                    Stats.Hp = Stats.MaxHp;
                }
                Health.Initialize(Stats.Hp, Stats.MaxHp, Stats.Gold);
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 6:
                anim.SetInteger("Index", 6);
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 7:
               // Time.timeScale = 0.10f;
                Stats.DamagePlus += 15f-Stats.Damage;
                SetText();
                anim.SetInteger("Index", 7);
                anim.SetBool("IsAttack", true);

                break;
            case 8:
                Sound.PlayerVoice(Audio[6]);
                Sound.Play();
                anim.SetInteger("Index", 8);
                Enchant = 6;
                Stats.Enchant = 1.25f;
                SetText();
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 9:
                Sound.PlayerVoice(Audio[11]);
                Sound.Play();
                anim.SetInteger("Index", 9);
                StartCoroutine(Objects.PlayerTurn());
               
                break;
            case 10:
                Sound.PlayerVoice(Audio[7]);
                Sound.Play();
                Stats.Endure = .25f;
                StartCoroutine(Objects.PlayerTurn());
                break;
            case 11:
                Stats.DamagePlus += 9999;
                SetText();
                break;
            case 12:
                Sound.PlayerVoice(Audio[13]);
                Sound.Play();
                anim.SetInteger("Index", 12);
                Stats.Damage += 5;
                SetText();
                StartCoroutine(Objects.PlayerTurn());
                break;
            default:
                break;
        }
        Stats.Armor += Stats.Endure;
    }

    public IEnumerator Move(GameObject target)
    {
        anim.SetBool("IsMoving", true);
       

         while (MoveToNextNode(new Vector3(target.transform.position.x - 2, target.transform.position.y, target.transform.position.z))) { yield return null; }
         yield return new WaitForSeconds(0.1f);
       
        Attack();
        //StartCoroutine(Clear());
        yield break;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 8f * Time.deltaTime));

        }
    }

    public IEnumerator Move()
    {
        anim.SetBool("IsMoving", true);
      

        while (MoveToNextNode(new Vector3(Objects.ObjectsList[1].transform.position.x - 2, Objects.ObjectsList[1].transform.position.y, Objects.ObjectsList[1].transform.position.z))) { yield return null; }
        yield return new WaitForSeconds(0.1f);
        
        Attack();
        //StartCoroutine(Clear());
        yield break;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 8f * Time.deltaTime));

        }
    }

    void Attack()
    {
        Stats.Damage = 50;
        //Time.timeScale = 0.10f;
        //Effect("Attack_01", this.transform.GetChild(1).gameObject);
        anim.SetBool("IsAttack", true);
    }


    public IEnumerator Clear()
    {

        
            while (MoveToNextNode(ReTrans)) { yield return null; }
            yield return new WaitForSeconds(0.1f);
        
        anim.SetBool("IsMoving", false);
        StartCoroutine(Objects.PlayerTurn());
        anim.SetInteger("Index", 0);

        Stats.DamagePlus = 0;
        AttackBox.radius = 0.7059865f;
        if (Index == 0)
            Stats.Armor += Stats.Endure;
        Index = 0;
        SetText();
        yield break;
    }
    bool MoveToNextNode(Vector3 goal)
    {

        return goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 120f * Time.deltaTime));

    }

    void OnDamage()
    {
        
        TakeDamage();
        if (anim.GetInteger("Index") != 6&&anim.GetInteger("Index") != 20 )
            Effect("Damage", this.transform.GetChild(0).gameObject,new Vector3(0.2f,0.2f),0f);
       
       
         Health.Initialize(Stats.Hp, Stats.MaxHp, Stats.Gold);
        if(Stats.Hp <=0)
        {
            anim.SetBool("IsDeath", true);
            Invoke("Death", 2f);
        }
        else
        {
            Invoke("OffDamage", 0.7f);
            anim.SetBool("IsHit", true);
        }
      
    }

    void Death()
    {
        
        Objects.Lost();
    }
    void OffDamage()
    {
        anim.SetBool("IsHit", false);
        
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            OnDamage();
        }
    }





}

