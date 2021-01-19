using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spider_Enemy : Enemy
{
    float Return;
    bool Provke = false;
    
    private void OnEnable()
    {
        Stats.Hp = 35;
        Stats.Damage = 3;
        Stats.Skill = 0;
        Stats.Gold = 100;
        Stats.MaxHp = 35;
        Stats.Rand = 0;
        Stats.Cool = 0;

       

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

    private void Start()
    {
        ReTrans = Trans.position;

        sound = new string[4] { "SPIDER_HITTEN", "SPIDER_JUMP", "SPIDER_DEATH", "SPIDER_ATTACK" };
        Audio = GetComponent<SoundScript>();
        SkillState.SkillS += Netuarl;


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
        Stats.Damage = Stats.Damage * 0.75f;
        Provke = true;
    }
    void PlayerTakeDamage()
    {
        if (player.Index == 6)
        {
            //Stats.Hp -= Stats.Damage;
            //if (Stats.Hp <= 0)
            //{
            //    StopAllCoroutines();
            //    Audio.SkeletonVoice(sound[2]);
            //    Audio.Play();
            //    player.Stats.Gold += Stats.Gold;
            //    this.anim.SetBool("IsDeath", true);
            //    Health.gameObject.SetActive(false);
            //}
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsMoving", false);
            AttackBox.enabled = false;

        }
        else
        {
            Audio.SpiderVoice(sound[3]);
            Audio.Play();
            var Dmg = (Stats.Damage - (player.Stats.Armor + player.Stats.ArmorPlus));
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
    // Update is called once per frame
    private void OnDestroy()
    {
        if (Stats.Cool > 0)
            player.Stats.Armor += 2;
        Player.TakeDamage -= PlayerTakeDamage;
        SkillState.SkillS -= Netuarl;
    }
    protected override void Update()
    {
        Health.Initialize(Stats.Hp, Stats.MaxHp, 0f);

        if (anim.GetBool("IsAttack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75f)
                AttackBox.enabled = false;
            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.35f)
            {

                AttackBox.enabled = true;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                anim.SetBool("IsAttack", false);
                if (Stats.Hp > 0)
                    StartCoroutine(Clear());
            }

        }

        base.Update();
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
            if (player.anim.GetInteger("Index") == 6)
            {
                Invoke("Corutine", 0.5f);
            }
            anim.SetBool("IsHit", true);
            Audio.SpiderVoice(sound[0]);
            Audio.Play();
            Invoke("OffDamage", 1.1f);
        }
        else if (Stats.Hp <= 0)
        {
            if (player.anim.GetInteger("Index") == 6)
            {
                Invoke("Death", 0.7f);
            }
            StopAllCoroutines();
            Audio.SpiderVoice(sound[2]);
            Destroy(this.icon);
            Audio.Play();
            player.Stats.Gold += Stats.Gold;
            this.anim.SetBool("IsDeath", true);
            Health.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

    }

    public void HealthSetUp(string name)
    {
        Health = GameObject.Find(name).GetComponent<Stats>();
        Health.statValue = Health.gameObject.transform.GetChild(0).GetComponent<Text>();

        Health.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z);
        Health.transform.localScale = new Vector3(0.1f, 0.1f);
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
            Icon("SkillPool", transform.GetChild(2).gameObject);
        }
        else
        {
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        }
    }
    public void State()
    {
        if (!Provke)
        {

            

            if (Stats.Rand == 0)
            {
                StartCoroutine(Move());
            }
            else if (Stats.Rand == 1 && Stats.Skill == 0)
            {
                Awakening();
            }
            else
            {
                Stats.Rand = 0;
                StartCoroutine(Move());
            }
        }
        if (Provke)
        {
            Stats.Rand = 0;
            Provke = false;
            StartCoroutine(Move());
        }
    }

    void Awakening()
    {

        Stats.Cool = 4;
        Stats.Skill = 5;
        
        player.Stats.Armor -= 2f;
        
        anim.SetBool("IsSkill", true);
        Audio.SpiderVoice(sound[1]);
        Audio.Play();
        StartCoroutine(Clear());

    }
    public IEnumerator Move()
    {

        anim.SetBool("IsMoving", true);


        while (MoveToNextNode(new Vector3(Objects.ObjectsList[0].position.x + 1, Objects.ObjectsList[0].position.y, Objects.ObjectsList[0].position.z)))
        {
            Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(Trans.position.x, Trans.position.y - 0.5f, Trans.position.z), 8f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        Attack();

        yield break;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (Trans.position = Vector3.MoveTowards(Trans.position, goal, 6f * Time.deltaTime));

        }
    }

    public void Attack()
    {
        Player.TakeDamage += PlayerTakeDamage;
        anim.SetBool("IsAttack", true);
    }

    public IEnumerator Clear()
    {

        if (Stats.Rand == 1)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            while (MoveToNextNode(ReTrans))
            {
                // Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(ReTrans.x, ReTrans.y - 0.5f, ReTrans.z), 120f * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
        Player.TakeDamage -= PlayerTakeDamage;
        SkillSt();
        
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsSkill", false);
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
    void SkillSt()
    {
        if (Stats.Skill > 0)
            Stats.Skill--;
        if (Stats.Cool >= 0)
            Stats.Cool--;
        if (Stats.Cool == 0)
        {
            player.Stats.Armor += 2;
            Return = 0;
        }
    }
    void OffDamage()
    {
        anim.SetBool("IsHit", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            OnDamage();
            FindObjectOfType<TimeStop>().StopTime(0.05f, 5, 0.1f);
        }
    }
}

