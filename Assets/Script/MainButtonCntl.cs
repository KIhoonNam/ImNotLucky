using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainButtonCntl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    GameManager gg;
    SoundScript sound;
    public GameObject[] Game;
    bool Op;
    public void OnEnable()
    {

        sound = GetComponent<SoundScript>();
        gg = FindObjectOfType<GameManager>();
      
    }
    public void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + " Manager"))
        {

            gg.LoadGame();
        }
       
    }
    public void LoadScene()
    {
        if (gg.StageState == "Stage1")
        {
            LoadSceneControl.LoadingScene("SampleScene");
        }
        else if(gg.StageState == "Stage2")
        {
            LoadSceneControl.LoadingScene("Stage2");
        }
        else if(gg.StageState == "Stage")
        {
            LoadSceneControl.LoadingScene("Stage Scene");
        }
        else if (gg.StageState == "Seller")
            LoadSceneControl.LoadingScene("Seller");
    }

    [System.Obsolete]
    public void OnPointerDown(PointerEventData eventData)
    {

        
        if (transform.name == "Start")
        {


            sound.Play();
                gg.Fade.Fade();
                Debug.Log("UP");
                Invoke("LoadScene", 1.0f);
            
        }
        if(transform.name == "Exit")
        {
            sound.Play(); 
            Application.Quit();
        }
        if(transform.name == "Option")
        {
            if (!Op)
            {
                Game[0].SetActive(false);
                Game[1].SetActive(false);
                Game[2].SetActive(true);
                Op = true;
            }
            else
            {
                Game[0].SetActive(true);
                Game[1].SetActive(true);
                Game[2].SetActive(false);
                Op = false;
            }
            sound.Play();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.name == "Start")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("START_BIG") as Sprite;

        if (transform.name == "Option")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("OPTION_BIG") as Sprite;
        if (transform.name == "Exit")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("EXIT_BIG") as Sprite;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.name == "Start")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("START") as Sprite;
        if (transform.name == "Option")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("OPTION") as Sprite;
        if (transform.name == "Exit")
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("EXIT") as Sprite;
    }

    
}
