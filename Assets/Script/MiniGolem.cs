using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGolem : Enemy
{
    bool Provoke;
    int Skill1 = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Stats.Hp = 80;
        Stats.Damage = 5;
        Stats.Skill = 0;
        Stats.Gold = 300;
        Stats.MaxHp = 80;
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

    private void Start()
    {
        ReTrans = Trans.position;
        SkillState.SkillS += Netuarl;
        sound = new string[] { "GOLEM_ATTACK,SKILL", "GOLEM_HEAL", "GOLEM_DEATH", "GOLEM_HITTEN" };

    }
    public void Die()
    {
        Effect("Kill", transform.GetChild(1).gameObject,1f);
        OnDamage();
    }
    void Netuarl()
    {
        
            Effect("Attack03", transform.GetChild(1).gameObject,1f);
      
    }
    public void Effect(string name, GameObject transform,float Scale)
    {
        Vector3 Position = new Vector3(transform.transform.position.x, transform.transform.position.y, transform.transform.position.z);
        var effect = Resources.Load<GameObject>("Effect/" + name);
        Effects = Instantiate(effect, Position, Quaternion.identity);
        Effects.transform.SetParent(GameObject.Find("Ef2").transform);
        Effects.transform.position = Position;
        Effects.transform.localScale = new Vector3(Scale, Scale);
        Effects.transform.rotation = Quaternion.Euler(0, 180, 0);


    }
    public void Do()
    {
        Stats.Damage = Stats.Damage * 0.75f;
        Provoke = true;
    }
    void PlayerTakeDamage()
    {
        if (player.Index == 6)
        {
            if (!anim.GetBool("IsCombo"))
            {
                anim.SetBool("IsAttack", false);
                anim.SetBool("IsMoving", false);
                AttackBox.enabled = false;
            }

        }
        else
        {
           
           
                Audio.GolemVoice(sound[0]);
                Audio.Play();
            
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
        Stats.Rand = Random.Range(0, 3);

        if (Stats.Rand == 0)
        {
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        }
        else if (Stats.Rand == 2 && Skill1 == 0)
        {
            
            Icon("AttackPool", transform.GetChild(2).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = (Stats.Damage+5) + "";
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
        if (!Provoke)
        {
           

            if (Stats.Rand == 0)
            {
                StartCoroutine(Move());
            }
            else if (Stats.Rand == 1 && Stats.Skill == 0)
            {
                Victory();
                
            }
            else if(Stats.Rand == 2 && Skill1 == 0)
            {
                ComboAttack();


                StartCoroutine(Move());
                    
            }
            else
            {
                StartCoroutine(Move());
            }
        }
        else if (Provoke)
        {
            Stats.Rand = 0;
            Provoke = false;
            StartCoroutine(Move());
        }
    }
    void ComboAttack()
    {
        anim.SetBool("IsCombo", true);
        Skill1 = 3;
        
    }
    void Victory()
    {
        Audio.GolemVoice(sound[1]);
        Audio.Play();
        Stats.Hp += 15;
        Stats.Skill = 4;
        anim.SetBool("IsSkill", true);
        StartCoroutine(Clear());
    }
    public void DoCoruTine()
    {
        StartCoroutine(Clear());
    }

    // Update is called once per frame
    protected override void Update()
    {

        Health.Initialize(Stats.Hp, Stats.MaxHp, 0f);

        if (anim.GetBool("IsAttack") || anim.GetBool("IsCombo"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                AttackBox.enabled = false;
            else if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.70f &&  anim.GetBool("IsAttack") == true)
            {
                AttackBox.enabled = true;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.53f && anim.GetBool("IsMoving") == false)
            {
                AttackBox.enabled = true;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                if (Stats.Hp > 0)
                {
                    if (anim.GetBool("IsCombo") && anim.GetBool("IsAttack"))
                    {
                        anim.SetBool("IsAttack", false);
                        anim.SetBool("IsMoving", false);


                    }
                    else
                    {
                        if (player.Index != 6 && anim.GetBool("IsAttack"))
                        {
                            anim.SetBool("IsAttack", false);


                            StartCoroutine(Clear());
                        }
                    }
                }

            }

        }

        base.Update();
    }
    private void OnDestroy()
    {
        Player.TakeDamage -= PlayerTakeDamage;
        SkillState.SkillS -= Netuarl;
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
        Effect("Damage", this.transform.GetChild(0).gameObject,0.5f);
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
            Audio.GolemVoice(sound[3]);
            Audio.Play();
            Invoke("OffDamage", 1.1f);
        }
        else if (Stats.Hp <= 0)
        {
            if (player.anim.GetInteger("Index") == 6)
            {
                Invoke("Death", 0.7f);
            }
            Audio.GolemVoice(sound[2]);
            Audio.Play();
            Destroy(this.icon);
            player.Stats.Gold += Stats.Gold;
            Health.gameObject.SetActive(false);
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


        while (MoveToNextNode(new Vector3(Objects.ObjectsList[0].position.x + 4, Objects.ObjectsList[0].position.y, Objects.ObjectsList[0].position.z))) {
            Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(Trans.position.x, Trans.position.y - 0.5f, Trans.position.z), 8f * Time.deltaTime); 
            yield return null; }
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
            //Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(ReTrans.x, ReTrans.y - 0.5f, ReTrans.z), 120f * Time.deltaTime); 
            yield return null; }
        yield return new WaitForSeconds(0.1f);
        SkillSt();
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsSkill", false);
        Player.TakeDamage -= PlayerTakeDamage;
        Stats.Damage = 5;
        Stats.Rand = 0;
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
        if (Skill1 > 0)
            Skill1--;
        Stats.Damage = 5;
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
