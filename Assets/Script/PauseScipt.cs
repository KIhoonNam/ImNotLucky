using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScipt : MonoBehaviour
{
    Canvas Puase;
    GameManager gg;
    SoundScript sound;
    
    // Start is called before the first frame update
    void Start()
    {
        Puase = GetComponent<Canvas>();
        gg = FindObjectOfType<GameManager>();
        sound = GetComponent<SoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Countinue()
    {
        sound.Play();
        Puase.enabled = false;
        Time.timeScale = 1;
    }

    public void New()
    {
        sound.Play();
        SaveManager.Delete(" Item");
        SaveManager.Delete(" Manager");
        
        SaveManager.Delete(" Player");
        Time.timeScale = 1;
        gg.Reset();
        Puase.enabled = false;
        LoadSceneControl.LoadingScene("MainScene");
        
    }

    public void Exit()
    {
        sound.Play();
        Puase.enabled = false;
        Application.Quit();
    }
}
