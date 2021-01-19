using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StageState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class StageManager : MonoBehaviour
{
    public static event CountDamageHandler CountDamage;
    public static event SkillCoolHandler SkillCool;
    Transform[] Objects;
    [HideInInspector]
    Itembuffer itembuffer;
    public List<Transform> ObjectsList = new List<Transform>();
     Button Bot;
    float Gold;
     public GameObject[] m_goPrefab = null;
     public GameObject[] m_go = null;
    public StageState State;
    public bool AttackTime = false;
     bool ButtonOn = false;
    public int count = 0;
    public int Stop = 0;
    string Rand;
    int RuneGold;
    public SpriteRenderer Sprite;
    GameManager gg;
    Player Character;
    Enemy Enemy;
    Enemy Enemy1;
    Enemy Enemy2;
    MouseCursor cur;
    private GameObject target;
  
    [SerializeField]
    private Text GoldValue = null;
    Canvas canvas;
    SoundScript sound;
    Image Turn;
    [SerializeField]
    Image Runeimage = null;

    [SerializeField]
    Text TextGold;
    InventoryScript Inven;

    public void AttackT()
    {
        AttackTime = true;
        cur.ATChange();
    }
    // Start is called before the first frame update
    void Start()
    {
        itembuffer = FindObjectOfType<Itembuffer>();
        Inven = GetComponent<InventoryScript>();
        sound = GetComponent<SoundScript>();
        gg = FindObjectOfType<GameManager>();
        cur = FindObjectOfType<MouseCursor>();
        
        if (gg.Monster1 != null)
        {
            var go = Resources.Load<GameObject>(gg.Monster1);
            m_go[0] = (go as GameObject).gameObject;
        }
        if (gg.Monster2 != null)
        {
            var go1 = Resources.Load<GameObject>(gg.Monster2);
            m_go[1] = (go1 as GameObject).gameObject;
        }
        if(gg.Monster3 != null)
        {
            var go2 = Resources.Load<GameObject>(gg.Monster3);
            m_go[2] = (go2 as GameObject).gameObject;
        }
        
        Bot = GameObject.Find("AttackButton").GetComponent<Button>() ;
      
        Turn = GameObject.Find("MY_TURN").GetComponent<Image>();
        ObjectsCreate();
        GameObject PlayerGo = Instantiate(m_goPrefab[0], ObjectsList[0].transform.position, Quaternion.Euler(0, 95, 0));
        Character = PlayerGo.GetComponent<Player>();
        State = StageState.START;
        StartCoroutine(StageSetup());
        GoldVal();
        sound.Play();
        if (gg.StageIndex == "Stage2")
        {
            Sprite.sprite = Resources.Load<Sprite>("BATTLE_STAGE_BACKGROUND_2");
            Sprite.transform.localScale = new Vector3(Sprite.transform.localScale.x, 2.2f, Sprite.transform.localScale.z);

        }
    }
    void EnemyIcon()
    {
        if (Enemy != null)
            Enemy.IconSet(gg.Monster1, Enemy);


        if (Enemy1 != null)
            Enemy1.IconSet(gg.Monster2, Enemy1);
        if (Enemy2 != null)
            Enemy2.IconSet(gg.Monster3, Enemy2);
    }
    void Enable()
    {
        Character.SetText();
        Character.anim.SetInteger("Index", 0);
        Turn.enabled = true;
        EnemyIcon();
        Invoke("DisEnanble", 2f);
    }
    void DisEnanble()
    {
        Turn.enabled = false;
        
    }
    IEnumerator StageSetup()
    {
       
        if (m_go[0] != null)
        {
            GameObject EnemyGo = Instantiate(m_go[0], ObjectsList[1].transform.position, Quaternion.Euler(0, 270, 0));
            Enemy = EnemyGo.GetComponent<Enemy>();
        }
        if (m_go[1] != null)
        {
            GameObject EnemyGo2 = Instantiate(m_go[1], ObjectsList[2].transform.position, Quaternion.Euler(0, 270, 0));
            Enemy1 = EnemyGo2.GetComponent<Enemy>();
        }
        if(m_go[2] != null)
        {
            GameObject EnemyGo3 = Instantiate(m_go[2], ObjectsList[3].transform.position, Quaternion.Euler(0, 270, 0));
            Enemy2 = EnemyGo3.GetComponent<Enemy>();
        }
        if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
            Character.LoadPlayer();
        Enable();
        Character.SetText();
        EnemySetUp();
        yield return new WaitForSeconds(1f);


       
        State = StageState.PLAYERTURN;
        
        gg.Fade.Panel.enabled = false;
    }
    private void EnemySetUp()
    {
        if (Enemy != null)
            Enemy.EnemySet(gg.Monster1, Enemy, "EnemyHp1");


        if (Enemy1 != null)
            Enemy1.EnemySet(gg.Monster2, Enemy1, "EnemyHp2");
        if (Enemy2 != null)
            Enemy2.EnemySet(gg.Monster3, Enemy2, "EnemyHp3");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTime)
        {
            
            target = GetClickedObject();
            
            if (target != null && target.tag != "Player")  //선택된게 나라면

            {
               
                Debug.Log(target.name);

                if (Character.Index == 4)
                {
                    Character.Sound.PlayerVoice(Character.Audio[12]);
                    Character.Sound.Play();
                    if (Enemy != null && Enemy.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.Index = 0;
                        Character.anim.SetInteger("Index", 4);
                        Enemy.Down(gg.Monster1, Enemy);
                        StartCoroutine(PlayerTurn());
                    }
                    else if (Enemy2 != null && Enemy2.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.Index = 0;
                        Character.anim.SetInteger("Index", 4);
                        Enemy2.Down(gg.Monster3, Enemy2);
                        StartCoroutine(PlayerTurn());
                    }
                    else if (Enemy1 != null && Enemy1.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.anim.SetInteger("Index", 4);
                        Character.Index = 0;
                        Enemy1.Down(gg.Monster2, Enemy1);
                        StartCoroutine(PlayerTurn());

                    }

                }
                else if (Character.Index == 11)
                {

                    if (Enemy != null && Enemy.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.anim.SetInteger("Index", 11);
                        Character.Index = 0;
                        Character.Stats.DamagePlus = 0;
                        Enemy.ToDie(gg.Monster1, Enemy);
                        StartCoroutine(PlayerTurn());
                    }
                    else if (Enemy2 != null && Enemy2.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.anim.SetInteger("Index", 11);
                        Character.Index = 0;
                        Character.Stats.DamagePlus = 0;
                        Enemy2.ToDie(gg.Monster3, Enemy2);
                        StartCoroutine(PlayerTurn());
                    }
                    else if (Enemy1 != null && Enemy1.transform == target.transform)
                    {
                        AttackTime = false;
                        Character.anim.SetInteger("Index", 11);
                        Character.Index = 0;
                        Character.Stats.DamagePlus = 0;
                        Enemy1.ToDie(gg.Monster2, Enemy1);
                        StartCoroutine(PlayerTurn());

                    }
                }
                else
                {
                    AttackTime = false;
                    StartCoroutine(Character.Move(target));
                }


            }
        }
    }
    public IEnumerator PlayerTurn()
    {

        if (Character.Index == 4 || Character.Index == 5)
            yield return new WaitForSeconds(3.5f);
        else if (Character.Index == 11)
            yield return new WaitForSeconds(2.5f);
        else
            yield return new WaitForSeconds(1.5f);
        if (Character.Index == 9)
        {
            if (Enemy != null)
            {

                Enemy.Count = 4;
                
            }
             if (Enemy2 != null)
            {

                Enemy2.Count = 4;
                
            }
             if (Enemy1 != null)
            {

                Enemy1.Count = 4;
               
            }

        }
        if ((!Enemy && !Enemy1 && !Enemy2) )
            
        {
            cur.PerChange();
            State = StageState.WON;
            Won();
        }
        else
        {
            
            AttackTime = false;
            cur.PerChange();
            State = StageState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
         
        }
    }

    public IEnumerator EnemyAttack()
    {
        if (State != StageState.ENEMYTURN)
            yield break;
        
        if (Enemy != null && Stop == 0 && Enemy.Stats.Hp >= 0)
        {
            Enemy.Go(gg.Monster1, Enemy);
            
        }
        else if((Enemy == null  || Enemy.Stats.Hp <= 0 )&& Stop == 0)
        {
            Stop++;
            StartCoroutine(EnemyAttack());
        }
        else if (Enemy1 != null && Stop == 1&& Enemy1.Stats.Hp >= 0)
        {
            Enemy1.Go(gg.Monster2, Enemy1);
            
        }
        else if ((Enemy1 == null || Enemy1.Stats.Hp <= 0) && Stop == 1)
        {
            Stop++;
            StartCoroutine(EnemyAttack());
        }
        else if (Enemy2 != null && Stop == 2&&Enemy2.Stats.Hp >= 0)
        {
            Enemy2.Go(gg.Monster3, Enemy2);
            
        }
        else if ((Enemy2 == null || Enemy2.Stats.Hp <= 0) && Stop == 2)
        {
            Stop++;
            StartCoroutine(EnemyAttack());
        }
        else if (Stop >= 3)
        {
            if (((Enemy2 && Enemy2.Stats.Hp <= 0) || !Enemy2)
                && ((Enemy1 && Enemy1.Stats.Hp <= 0) || !Enemy1)
                && ((Enemy && Enemy.Stats.Hp <= 0) || !Enemy))
            {
                Invoke("ChangeState", 1.5f);
                Stop = 0;
            }
            else
                ChangeState();
        }

    }
    void ChangeState()
    {
        if ((!Enemy && !Enemy1 && !Enemy2))
        {
            State = StageState.WON;
            Won();
        }
        else if(Character.Stats.Hp >0)
        {
            Stop = 0;
            if (count == 3)
            {
                CountDamage();

            }
            count = 0;
            ButtonOn = false;
            Bot.interactable = true;
            Enable();
            State = StageState.PLAYERTURN;
            SkillCool();
            if (Character.Stats.ArmorPlus >= 1000)
                Character.Stats.ArmorPlus -= 1000;

       
        }
    }
    private void OnDestroy()
    {
        
    }
    void GoldVal()
    {
        GoldValue.text = null;
        Gold = 0;
        if (Enemy != null)
        {
            Gold += Enemy.EnemyGold();
        }

        if (Enemy1 != null)
        {
            Gold += Enemy1.EnemyGold();
        }

        if (Enemy2 != null)
        {
            Gold += Enemy2.EnemyGold();
        }
        
         
    }

    public void OnAttackButton()
    {
        
        if (State != StageState.PLAYERTURN)
            return;
        if (!ButtonOn && !AttackTime)
        {
            cur.ATChange();
            Bot.interactable = false;
            ButtonOn = true;
            AttackTime = true;
        }
    }

    void ObjectsCreate()
    {
        ObjectsList.Clear();
        Objects = GetComponentsInChildren<Transform>();

        foreach (Transform child in Objects)
        {
            if (child != this.transform)
            {
                ObjectsList.Add(child);
            }
        }
    }


    void Rune()
    {
        int Return = Random.Range(0, 100);
        if (gg.Boss == 0)
        {
            
            if ((Return <= 10 && gg.BattleState != "Uniq") ||
                (Return <= 30 && gg.BattleState == "Uniq"))
                Chance();
        }
        else if(gg.Boss == 1)
        {
            
            if ((Return <= 30 && gg.BattleState != "Uniq") ||
               (Return <= 50 && gg.BattleState == "Uniq"))
                Chance();
        }
        else if(gg.Boss == 2)
        {
            
            if ((Return <= 50 && gg.BattleState != "Uniq") ||
               (Return <= 70 && gg.BattleState == "Uniq"))
                Chance();
        }
        else if(gg.Boss==3)
        {
            Chance();
        }


    }

    void Chance()
    {
        if (gg.StageIndex == "Stage1")
        {
            RuneGold = Random.Range(0, 6);
            Rand = itembuffer.items[RuneGold].Name;
            for (int i = 0; i < Inven.PlayerItem.Length; i++)
            {
                if (Rand == Inven.PlayerItem[i].name)
                {
                    Gold += itembuffer.items[RuneGold].gold;
                    Rand = null;
                    TextGold.text = itembuffer.items[RuneGold].gold + "";
                    TextGold.enabled = true;
                }
            }
            if (Rand != null)
            {
                for (int i = 0; i < Character.RN.Length; i++)
                {
                    if (Rand == Character.RN[i].name)
                    {
                        Gold += itembuffer.items[RuneGold].gold;
                        Rand = null;
                        TextGold.text = itembuffer.items[RuneGold].gold + "";
                        TextGold.enabled = true;
                    }
                }
            }
            if (Rand != null)
            {
                Runeimage.sprite = Resources.Load<Sprite>(Rand);
                Runeimage.name = Rand;
                Runeimage.enabled = true;
                for (int i = 0; i < Inven.PlayerItem.Length; i++)
                {
                    if (Inven.PlayerItem[i].name == null || Inven.PlayerItem[i].name == "")
                    {
                        Inven.PlayerItem[i].name = Rand;
                        break;
                    }
                }
                Inven.SaveItem();
            }

        }
        else if (gg.StageIndex == "Stage2")
        {
            RuneGold = Random.Range(0, 12);
            Rand = itembuffer.items[RuneGold].Name;
            for (int i = 0; i < Inven.PlayerItem.Length; i++)
            {
                if (Rand == Inven.PlayerItem[i].name)
                {
                    Gold += itembuffer.items[RuneGold].gold;
                    Rand = null;
                    TextGold.text = itembuffer.items[RuneGold].gold + "";
                    TextGold.enabled = true;
                }
            }
            if (Rand != null)
            {
                for (int i = 0; i < Character.RN.Length; i++)
                {
                    if (Rand == Character.RN[i].name)
                    {
                        Gold += itembuffer.items[RuneGold].gold;
                        Rand = null;
                        TextGold.text = itembuffer.items[RuneGold].gold + "";
                        TextGold.enabled = true;
                    }
                }
            }
            if (Rand != null)
            {
                Runeimage.sprite = Resources.Load<Sprite>(Rand);
                Runeimage.name = Rand;
                Runeimage.enabled = true;
                for (int i = 0; i < Inven.PlayerItem.Length; i++)
                {
                    if (Inven.PlayerItem[i].name == null || Inven.PlayerItem[i].name == "")
                    {
                        Inven.PlayerItem[i].name = Rand;
                        break;
                    }
                }
                Inven.SaveItem();
            }
        }
    }
    void Won()
    {
        if (State != StageState.WON)
            return;
        Rune();
        GoldValue.text = Gold + "";
        Character.SavePlayer();
        if (gg.StageIndex == "Stage1")
        gg.StageState = "Stage1";
        else if (gg.StageIndex == "Stage2")
            gg.StageState = "Stage2";
        gg.SaveGame();
        canvas = GameObject.Find("RewardCan").GetComponent<Canvas>();
        canvas.enabled = true;
        sound.BATTLE_CLEAR();
        sound.Play();
    }

    public void Lost()
    {
        GameState.Over();
    }
    private GameObject GetClickedObject()

    {

        RaycastHit hit;

        GameObject target = null;



        var position = new Vector3(Input.mousePosition.x/3, Input.mousePosition.y, Input.mousePosition.z);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 





        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인

        {

            //있으면 오브젝트를 저장한다.

            target = hit.collider.gameObject;

        }



        return target;

    }

}

