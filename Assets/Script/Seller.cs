using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seller : MonoBehaviour
{
    GameManager gg;
    SoundScript sound;
    // Start is called before the first frame update
    private void Start()
    {
        gg = FindObjectOfType<GameManager>();
        sound = GetComponent<SoundScript>();
    }

    public void  ExitDoor()
    {
        sound.Play();
        if (gg.StageIndex == "Stage1")
        {
            gg.StageState = "Stage1";
            
            gg.SaveGame();
            LoadSceneControl.LoadingScene("SampleScene");
        }
        else if (gg.StageIndex == "Stage2")
        {
            gg.StageState = "Stage1";
            gg.SaveGame();
            LoadSceneControl.LoadingScene("Stage2");
        }
      
    }
    // Update is called once per frame
    void Update()
    {
     

    }
}
