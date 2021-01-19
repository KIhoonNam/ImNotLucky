using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    public FadeScript Fade;
    public int steps;
    public int Correct;
    public int Boss;
    public bool Start = false;
    public bool Stop = false;
    public bool isMoving = false;
    public bool EndMoving = false;
    public int routePosition;
    public string StageName = null;
    public string Monster1 = null;
    public string Monster2 = null;
    public string Monster3 = null;
    public string StageState = "NULL";
    public string ChoiceSeller = null;
    public Vector3 PlayerPosition = new Vector3(0.7f,0f,1.26f);
    public string State = "Null";
    public string ReState = null;
    public Route currentRoute;
    public bool Break;
    public bool Guard1State = false;
    public bool Guard2State = false;
    public string StageIndex = null;
    public bool PlayStop = false;
    public string BattleState = null;
    public bool SellerCome = false;
    Canvas Pause;
    SoundScript sound;
    

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Fade = GetComponentInChildren<FadeScript>();
        Pause = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        sound = GetComponent<SoundScript>();
        Reset();
        DontDestroyOnLoad(gameObject);
        
       
    }

    

    public void SaveGame()
    {
        SaveManager.ManagerSave(this);
        
    }
    public void Stage2()
    {
        steps = 0;
        Correct = 0;
        Boss = 0;
        Start = false;
        Stop = false;
        isMoving = false;
        EndMoving = false;
        PlayerPosition = new Vector3(-1.45f, 0, 1.98f);
        Boss = 0;
        State = "Null";
        StageState = "Stage2";
        StageIndex = "Stage2";
        State = "Null";
        ReState = null;
        Break = false;
        Guard1State = false;
        Guard2State = false;
        ChoiceSeller = null;
        routePosition = 0;
        currentRoute = null;

    }
    public void Reset()
    {
        
        steps = 0;
        Correct = 0;
        Boss = 0;
        Start = false;
        Stop = false;
        isMoving = false;
        EndMoving = false;
        routePosition = 0;
        PlayerPosition.x = 0.82f;
        PlayerPosition.y = 0f;
        PlayerPosition.z = 1.16f;
        State = "Null";
        ReState = null;
        Break = false;
        StageName = null;
        Monster1 = null;
        Monster2 = null;
        Monster3 = null;
        StageState = "Stage1";
        Guard1State = false;
        Guard2State = false;
        ChoiceSeller = null;
        StageIndex = null;
}
    public void LoadGame()
    {
        GameManagerData Manager = SaveManager.ManagerLoad();
        
        steps = Manager.steps;
        Correct = Manager.Correct;
        Boss = Manager.Boss;
        Start = Manager.Start;
        Stop = Manager.Stop;
        isMoving = Manager.isMoving;
        EndMoving = Manager.EndMoving;
        routePosition = Manager.routePosition;
        PlayerPosition.x = Manager.PlayerPositionx;
        PlayerPosition.y = Manager.PlayerPositiony;
        PlayerPosition.z = Manager.PlayerPositionz;
        State = Manager.State;
        ReState = Manager.ReState;
        Break = Manager.Break;
        StageName = Manager.StageName;
        Monster1 = Manager.Monster1;
        Monster2 = Manager.Monster2;
        Monster3 = Manager.Monster3;
        StageState = Manager.StageState;
        Guard1State = Manager.Guard1State;
        Guard2State = Manager.Guard2State;
        StageIndex = Manager.StageIndex;
        BattleState = Manager.BattleState;
        ChoiceSeller = Manager.ChoiceSeller;
        SellerCome = Manager.SellerCome;
        // currentRoute = Manager.currentRoute;
        // Debug.Log(currentRoute);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pause.enabled)
            {
                sound.Play();
                Pause.enabled = true;
                Time.timeScale = 0;
            }
            else if (Pause.enabled)
            {
                Pause.enabled = false;
                Time.timeScale = 1;
            }
        }
    }



}
