using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhino_Enemy : Enemy
{

    bool Provoke;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Stats.Hp = 30;
        Stats.Damage = 3;
        Stats.Skill = 0;
        Stats.Gold = 40;
        Stats.MaxHp = 30;
        Stats.Rand = 0;
        Stats.Cool = 0;
        Audio = GetComponent<SoundScript>();

        if (Trans == null)
            Trans = GetComponent<Transform>();
        if (anim == null)
            anim = GetComponent<Animator>();
        if (AttackBox == null)
            AttackBox = GetComponentInChildren<SphereCollider>();
        if (player == null)
            player = FindObjectOfType<Player>();
        Objects = GameObject.Find("StateSystem").GetComponent<StageManager>();


    }

    private  void Start()
    {
        ReTrans = Trans.position;
        SkillState.SkillS += Netuarl;
        sound = new string[] { "RIHNO_ATTACK", "RIHNO_BITE", "RIHNO_DEATH", "RIHNO_HITTEN" };

    }
    public void Die()
    {
        Effect("Kill", transform.GetChild(1).gameObject);
        OnDamage();
    }
    void Netuarl()
    {
        
            Effect("Attack03", transform.GetChild(1).gameObject);
      
    }
    public void Do()
    {
        Stats.Damage = 3;
        Stats.Damage = Stats.Damage * 0.75f;
        Provoke = true;
    }
    void PlayerTakeDamage()
    {
        if (player.Index == 6)
        {
            //Stats.Hp -= Stats.Damage;
            //if (Stats.Hp <= 0)
            //{
            //    Audio.RhinoVoice(sound[2]);
            //    Audio.Play();
            //    player.Stats.Gold += Stats.Gold;
            //    Health.gameObject.SetActive(false);
            //    StopAllCoroutines();
            //    this.anim.SetBool("IsDeath", true);
            //}
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsMoving", false);
            AttackBox.enabled = false;

        }
        else
        {
            if(Stats.Rand == 1)
            {
                Audio.RhinoVoice(sound[1]);
                Audio.Play();
            }
            else if(Stats.Rand == 0)
            {
                Audio.RhinoVoice(sound[0]);
                Audio.Play();
            }
            var Dmg = (Stats.Damage - player.Stats.Armor - player.Stats.ArmorPlus);
            var Rand = Random.Range(0, 100);
            if (Dmg <= 0 || Rand < 5)
            {
                Dmg = 0;
                player.anim.SetInteger("Index", 20);
            }
            Damage("DamageText", player.gameObject);
            DamageText.GetComponent<Text>().text = "-" + Dmg;
            player.Stats.Hp -= Dmg;
            
        }
    }
    private void OnDestroy()
    {
        Player.TakeDamage -= PlayerTakeDamage;
        SkillState.SkillS -= Netuarl;
    }
    void Icon(string name, GameObject transform)
    {
        Vector3 Position = new Vector3(transform.transform.position.x, transform.transform.position.y, transform.transform.position.z);
        var effect = Resources.Load<GameObject>(name);
        icon = Instantiate(effect, Position, Quaternion.identity);
        icon.transform.SetParent(GameObject.Find("Ef2").transform);
        icon.transform.position = Position;
        icon.transform.localScale = new Vector3(0.05f, 0.05f);
       

    }
    public void IconState()
    {
        Stats.Rand = Random.Range(0, 2);

        if (Stats.Rand == 0)
        {
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        }
        else if (Stats.Rand == 1 && Stats.Skill == 0)
        {
            Stats.Damage += 1;
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        }
        else
        {
            Stats.Rand = 0;
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        }
    }
    public void State()
    {
        if (!Provoke)
        {          

            if (Stats.Rand == 0)
            {
                StartCoroutine(Move());
            }
            else if (Stats.Rand == 1 && Stats.Skill == 0)
            {
                Awakening();
                StartCoroutine(Move());
            }
            else
            {
                StartCoroutine(Move());
            }
        }
        else if(Provoke)
        {
            Stats.Rand = 0;
            Provoke = false;
            StartCoroutine(Move());
        }
    }

    void Awakening()
    {
              
        
        Stats.Skill = 3;
        anim.SetBool("IsSkill", true);
    }
    // Update is called once per frame
    protected override void Update()
    {

        Health.Initialize(Stats.Hp, Stats.MaxHp, 0f);

        if (anim.GetBool("IsAttack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75f)
                AttackBox.enabled = false;
            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.25f)
            {
                AttackBox.enabled = true;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                anim.SetBool("IsAttack", false);
                anim.SetBool("IsSkill", false);

                
               StartCoroutine(Clear());

            }
            
        }

        base.Update();
    }
    void Effect(string name, GameObject transform)
    {
        Vector3 Position = new Vector3(transform.transform.position.x, transform.transform.position.y, transform.transform.position.z);
        var effect = Resources.Load<GameObject>("Effect/" + name);
        Effects = Instantiate(effect, Position, Quaternion.identity);
        Effects.transform.SetParent(GameObject.Find("Ef2").transform);
        Effects.transform.position = Position;
        Effects.transform.localScale = new Vector3(0.2f, 0.2f);
        Effects.transform.rotation = Quaternion.Euler(0, 180, 0);

    }
    void Corutine()
    {
        StartCoroutine(Clear());
    }
    void Death()
    {
        Objects.Stop++;
        StartCoroutine(Objects.EnemyAttack());
    }
    void OnDamage()
    {
        Effect("Damage", this.transform.GetChild(0).gameObject);
        PlayerSound();
        player.Sound.Play();
        Stats.Hp -= ((player.Stats.Damage + player.Stats.DamagePlus) * player.Stats.Enchant);
        var Dmg = (player.Stats.Damage + player.Stats.DamagePlus) * player.Stats.Enchant;
        Damage("DamageText", this.gameObject);
        DamageText.GetComponent<Text>().text = "-" + Dmg;
        if (Stats.Hp > 0)
        {
            if(player.anim.GetInteger("Index") == 6)
            {
                Invoke("Corutine", 0.5f);
            }
            anim.SetBool("IsHit", true);
            Audio.RhinoVoice(sound[3]);
            Audio.Play();
            Invoke("OffDamage", 1.1f);
        }
        else if(Stats.Hp <= 0)
        {
            Audio.RhinoVoice(sound[2]);
            Audio.Play();
            player.Stats.Gold += Stats.Gold;
            Health.gameObject.SetActive(false);
            Destroy(this.icon);
            if (player.anim.GetInteger("Index") == 6)
            {
                Invoke("Death", 0.7f);
            }
            this.anim.SetBool("IsDeath", true);
        }

    }
    public void HealthSetUp(string name)
    {
        Health = GameObject.Find(name).GetComponent<Stats>();
        Health.statValue = Health.gameObject.transform.GetChild(0).GetComponent<Text>();
        Health.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z);
        Health.transform.localScale = new Vector3(0.1f, 0.1f);
    }



    public IEnumerator Move()
    {
        
        anim.SetBool("IsMoving", true);
       

        while (MoveToNextNode(new Vector3(Objects.ObjectsList[0].position.x + 2, Objects.ObjectsList[0].position.y, Objects.ObjectsList[0].position.z))) {
            Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(Trans.position.x, Trans.position.y - 0.5f, Trans.position.z), 8f * Time.deltaTime);
            yield return null; 
        }
        yield return new WaitForSeconds(0.1f);
       
        Attack();
        //StartCoroutine(Clear());
        yield break;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 8f * Time.deltaTime));

        }
    }

    public void Attack()
    {
        Player.TakeDamage += PlayerTakeDamage;
        anim.SetBool("IsAttack", true);
    }

    public IEnumerator Clear()
    {

        
        while (MoveToNextNode(ReTrans)) { 
             
            yield return null; }
        yield return new WaitForSeconds(0.1f);
        SkillSt();
        anim.SetBool("IsMoving", false);
        Player.TakeDamage -= PlayerTakeDamage;
        Stats.Damage = 3;
        yield return new WaitForSeconds(1f);
        Objects.Stop++;
        StartCoroutine(Objects.EnemyAttack());
        yield break;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(ReTrans.x, ReTrans.y - 0.5f, ReTrans.z), 120f * Time.deltaTime)) &&
                 (goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 120f * Time.deltaTime)));

        }
    }
    void SkillSt()
    {
        if (Stats.Skill > 0)
            Stats.Skill--;
         Stats.Damage = 3;
    }
    void OffDamage()
    {
        anim.SetBool("IsHit", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerAttack")
        {
            OnDamage();
            GameObject.FindObjectOfType<TimeStop>().StopTime(0.05f, 5, 0.1f);
        }
    }
}
