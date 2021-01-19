using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InfoScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public static event CheckPointHandler CheckPointer;
    public static event GuardMoveHandler Stop;
   
    Character Player;
    Stage2 Stage2;
    PlayerSprite Character;
    GameManager gg;
    SoundScript sound;
    [SerializeField]
    private Image NotG = null;
    

    void Start()
    {
        Stage2 = FindObjectOfType<Stage2>();
        gg = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<Character>();
        Character = FindObjectOfType<PlayerSprite>();
        sound = GetComponent<SoundScript>();
    }
    void NotGF()
    {
        NotG.enabled = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.name == "InfoButtonR")
        {
            sound.Button();
            sound.Play();
            if (GameObject.Find("InfoButtonL").GetComponent<Image>().enabled == false)
            {

                GameObject.Find("InfoButtonL").GetComponent<Image>().enabled = true;
                GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("CHOICE_YES") as Sprite;
            }
            else if(GameObject.Find("InfoButtonL").GetComponent<Image>().enabled == true)
            {
                
                if (GameObject.Find("InfoPage").GetComponent<Image>().sprite.name == "CHOICE_YES")
                {
                    if (Character.Stats.Gold >= 400)
                    {
                        Character.Stats.Gold -= 400;
                        Character.Health.Initialize(Character.Stats.Hp, Character.Stats.MaxHp, Character.Stats.Gold);
                        Character.SavePlayer();
                        CheckPointer();
                        GameObject.Find("EXit").GetComponent<Image>().enabled = true;
                        GameObject.Find("InfoButtonL").GetComponent<Image>().enabled = false;
                        GameObject.Find("InfoButtonR").GetComponent<Image>().enabled = false;
                        GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO") as Sprite;
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



                else if (GameObject.Find("InfoPage").GetComponent<Image>().sprite.name == "CHOICE_NO")
                {
                    GameObject.Find("EXit").GetComponent<Image>().enabled = true;
                    GameObject.Find("InfoButtonL").GetComponent<Image>().enabled = false;
                    GameObject.Find("InfoButtonR").GetComponent<Image>().enabled = false;
                    GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO") as Sprite;
                    if (gg.StageName == "Guard")
                    {
                        

                        Stop();
                        Invoke("Return", 1f);
                    }
                    else

                    {
                        Stop();
                        if (Player != null)
                            Player.Cancle();
                        else if (Stage2 != null)
                            Stage2.Cancle();
                    }
                }
                
                
            }
        }
        if (transform.name == "InfoButtonL")
        { 
            sound.Play();
            this.GetComponent<Image>().enabled = false;
            GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_1") as Sprite;
            
        }
        if (transform.name == "EXit")
        {
            sound.Play();
            GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_1") as Sprite;
            GameObject.Find("GuardCanvas").GetComponent<Canvas>().enabled = false;
            GameObject.Find("EXit").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoButtonL").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoButtonR").GetComponent<Image>().enabled = true;
        }
            
    }
    void Return()
    {
        if (Player != null)
        {
            gg.StageState = "Stage1";
            Player.Return();
        }
        else if (Stage2 != null)
        {
            gg.StageState = "Stage2";
            Stage2.Return();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.name == "CP" && GameObject.Find("InfoButtonL").GetComponent<Image>().enabled)
            GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("CHOICE_YES") as Sprite;
        if(transform.name == "CNP" && GameObject.Find("InfoButtonL").GetComponent<Image>().enabled)
            GameObject.Find("InfoPage").GetComponent<Image>().sprite = Resources.Load<Sprite>("CHOICE_NO") as Sprite;
        if (transform.name == "InfoButtonR")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_BUTTON_PUSH(RIGHT)") as Sprite;
        if (transform.name == "InfoButtonL")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_BUTTON_PUSH(LEFT)") as Sprite;
        if(transform.name == "EXit")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("EXIT_PRESS_BUTTON") as Sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.name == "InfoButtonR")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_BUTTON(RIGHT)") as Sprite;
        if (transform.name == "InfoButtonL")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("INFO_BUTTON(LEFT)") as Sprite;
        if (transform.name == "EXit")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("EXIT_BUTTON") as Sprite;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
