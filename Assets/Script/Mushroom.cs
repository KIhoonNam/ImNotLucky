using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mushroom : Enemy
{
    float Return;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Stats.Hp = 40;
        Stats.Damage = 4;      
        Stats.Gold = 100;
        Stats.MaxHp = 40;
        Stats.Skill = 0;
        StageManager.CountDamage += DamageUp;
        Return = Stats.Damage;
        
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
        Audio = GetComponent<SoundScript>();
        ReTrans = Trans.position;
        sound = new string[] { "MUSHEROOM_ATTACK", "MUSHEROOM_DEATH", "MUSHEROOM_HITTEN", "MUSHEROOM_SKILL" };
        SkillState.SkillS += Netuarl;

    }
    public void Die()
    {
        Effect("Kill", transform.GetChild(4).gameObject);
        OnDamage();
    }
    void Netuarl()
    {
        
            Effect("Attack03", transform.GetChild(4).gameObject);
       
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
       

       
            Icon("AttackPool", transform.GetChild(5).gameObject);
            icon.transform.GetChild(0).GetComponent<Text>().text = Stats.Damage + "";
        
   
    }
    public void State()
    {
      
            StartCoroutine(Move());
      

    }

    public void HealthSetUp(string name)
    {
        Health = GameObject.Find(name).GetComponent<Stats>();
        Health.statValue = Health.gameObject.transform.GetChild(0).GetComponent<Text>();
        Health.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z);
        Health.transform.localScale = new Vector3(0.1f, 0.1f);
    }
    public void Do()
    {
        Stats.Damage = Stats.Damage * 0.75f;
    }
    void DamageUp()
    {
        Audio.MushVoice(sound[3]);
        Audio.Play();
        Stats.Damage += 1;
        Return = Stats.Damage;
        Debug.Log(Stats.Damage);
    }
    void PlayerTakeDamage()
    {
        if (player.Index == 6)
        {


            anim.SetBool("IsAttack", false);
            anim.SetBool("IsMoving", false);
            AttackBox.enabled = false;

        }
        else
        {
            Audio.MushVoice(sound[0]);
            Audio.Play();
            var Dmg = (Stats.Damage - player.Stats.Armor - player.Stats.ArmorPlus);
            var Rand = Random.Range(0, 100);
            if (Dmg <= 0 || Rand < 5)
            {
                Dmg = 0;
                player.anim.SetInteger("Index", 20);
            }
            else
                Objects.count++;
            Damage("DamageText", player.gameObject);
            DamageText.GetComponent<Text>().text = "-" + Dmg;
            player.Stats.Hp -= Dmg;
           
        }
    }
    // Update is called once per frame
    protected override void Update()
    {


        Health.Initialize(Stats.Hp, Stats.MaxHp,0f);
        
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
                if (player.Index != 6)
                {


                    if (Stats.Hp > 0)
                    {
                        anim.SetBool("IsAttack", false);
                        StartCoroutine(Clear());
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
        StageManager.CountDamage -= DamageUp;
    }
    void OnDamage()
    {
        Effect("Damage", transform.GetChild(3).gameObject);
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
           
            anim.SetBool("IsHit",true);
            Audio.MushVoice(sound[2]);
            Audio.Play();
            Invoke("OffDamage", 1.0f);
        }
        else if (Stats.Hp <= 0)
        {
            if (player.anim.GetInteger("Index") == 6)
            {

                Invoke("Death", 0.7f);
            }
            StopAllCoroutines();
            Audio.MushVoice(sound[1]);
            Audio.Play();
            Invoke("OffDamage", 1.0f);
            Health.gameObject.SetActive(false);
            player.Stats.Gold += Stats.Gold;
            Destroy(this.icon);
            anim.SetBool("IsDeath", true);


        }

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



    public IEnumerator Move()
    {


        anim.SetBool("IsMoving", true);

        while (MoveToNextNode(new Vector3(Objects.ObjectsList[0].position.x + 2, Objects.ObjectsList[0].position.y, Objects.ObjectsList[0].position.z))) {
            Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(Trans.position.x, Trans.position.y - 0.5f, Trans.position.z), 8f * Time.deltaTime);
            yield return null; }
        yield return new WaitForSeconds(0.1f);

        Attack();
        
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
        Stats.Skill++;
        
        Debug.Log(Objects.count);
    }

    public IEnumerator Clear()
    {


        while (MoveToNextNode(ReTrans)) {
           // Health.transform.position = Vector3.MoveTowards(Health.transform.position, new Vector3(ReTrans.x, ReTrans.y - 0.5f, ReTrans.z), 120f * Time.deltaTime); 
            yield return null; }
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("IsMoving", false);
        Player.TakeDamage -= PlayerTakeDamage;
        Stats.Damage = Return;
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

    void OffDamage()
    {  if (Stats.Hp <= 0)
        {
            Destroy(this.gameObject);
        }
       
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
