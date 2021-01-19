using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasOnOff : MonoBehaviour
{
    Canvas Inven;
    FadeScript Fade;
    SoundScript sound;
    Slider image;
    GameManager gg;
    private void Awake()
    {
        gg = FindObjectOfType<GameManager>();
        Inven = GetComponent<Canvas>();
        Fade = FindObjectOfType<FadeScript>();

        sound = GetComponent<SoundScript>();
        
    }
    private void Start()
    {
        if(this.GetComponent<Canvas>() && this.name != "DiceCanvas")
            Inven.enabled = false;
        image = FindObjectOfType<Slider>();

        
            
    }

    public void On()
    {

        Inven.enabled = true;
   
        if (sound != null)
        {
            sound.UI_OPEN();
            if (this.name == "RewardCan")
                sound.WIn();

            sound.Play();
           
        }
        if(this.name == "Inventory" || this.name == "DiceInventory")
        {
           
            Time.timeScale = 0f;
        }
        if (this.name == "DiceCanvas")
            GameObject.Find("RollCanvas").GetComponent<Canvas>().enabled = true;


    }

    public void off()
    {
        if (sound != null)
        {
            if (this.tag != "Respawn")
            {
                sound.Button();
                sound.Play();
            }
        }
        if (this.name == "Inventory" || this.name == "DiceInventory")
        {
            
            Time.timeScale = 1f;
            
        }
        Inven.enabled = false;
        Debug.Log("Dfdf");
    }

    public void Scene()
    {
        Fade.Fade();
        Invoke("SceneChan", 1f);
    }
    void SceneChan()
    {
        if (gg.StageIndex == "Stage1")
        {
            LoadSceneControl.LoadingScene("SampleScene");
        }
        else if(gg.StageIndex == "Stage2")
        {
            LoadSceneControl.LoadingScene("Stage2");
        }
    }

    [System.Obsolete]
    public void Option()
    {
        if (sound != null)
        {
            sound.Button();
            sound.Play();

        }
        image = transform.FindChild("Slider").GetComponent<Slider>();
        if (!image.gameObject.activeSelf)
            image.gameObject.SetActive(true);
        else
            image.gameObject.SetActive(false);

    }
}
