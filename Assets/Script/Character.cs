using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    
    GameManager gg;
    Vector3 nextPos;
    Transform Player;
    Animator Anim;
    RouteManager RM;
 
    public static event GuardHandler GuardOn;
    public static event ResultHandler Result;
    NodeState State;
    bool GuardPass = false;
    bool GuardState = false;
    [SerializeField]
    Canvas GuardCanvas = null;
    SoundScript sound;
    [SerializeField]
    CanvasOnOff RollCanvas = null;
 
    void OnEnable()
    {

        gg = FindObjectOfType<GameManager>();

        sound = GetComponent<SoundScript>();
    }
  
    void Start()
    {

        Di.DiceRoll += Diceroll;

        Player = GetComponentInChildren<Transform>();

        Anim = GetComponentInChildren<Animator>();
      
 
        RM = FindObjectOfType<RouteManager>();

        State = FindObjectOfType<NodeState>();
     
  
        if (transform.position != gg.PlayerPosition)
        {
            transform.position = gg.PlayerPosition;
        }
    
        if (gg.State == "Boss1")
        {
            gg.Stage2();
            gg.SaveGame();
            LoadSceneControl.LoadingScene("Stage2");
        }

        gg.StageIndex = "Stage1";
    
        gg.Break = false;
        gg.BattleState = null;
        gg.currentRoute = null;

        if (GuardOn == null && gg.StageName != "Guard")
                Moving();
    
        if (gg.Start == false && gg.steps<=0)
        {

            DiceCheckZoneScript.Stop = false;
         
            
            
            RollCanvas.On();
            
           
        }


    }
    void DiceSteps()
    {
        DiceStat.DiceS -= DiceSteps;
       
        gg.steps = DiceCheckZoneScript.diceNumber;
        Debug.Log("Dice Roll" + gg.steps);
        Result(gg.steps,null);
        
        if (gg.Stop == true && gg.ChoiceSeller == null)
        {
            
            StartCoroutine(this.Move());
            
        }
    }
    void Diceroll()
    {

        DiceStat.DiceS += DiceSteps;
        gg.Start = true;
        gg.EndMoving = true;
    }

    private void OnDestroy()
    {
        
            DiceStat.DiceS -= DiceSteps;
        
            Di.DiceRoll -= Diceroll;
    }
    private void Update()
    {

        if(Time.timeScale == 0)
        {
          
            sound.Stop();
        }
        

        if (gg.isMoving)
        {
         
            Anim.SetBool("IsMoving", true);
        }
        else if (!gg.isMoving)
        {
         
            Anim.SetBool("IsMoving", false);
        }
        //if (Input.GetKeyDown(KeyCode.Space) && !gg.isMoving && !gg.EndMoving && !gg.Break)
        //{
        //    Di.DiceRoll += Diceroll;



        //}

        if (!GuardCanvas.enabled)
        {



            if (!gg.Stop && gg.Start && gg.steps > 0)
            {
                if (Input.GetKeyDown(KeyCode.W) && gg.ChoiceSeller == "SellerCorner1")
                {
                    gg.ChoiceSeller = null;
                    StartCoroutine(SellerMove1());
                }

                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (gg.ChoiceSeller == null || gg.ChoiceSeller == "")
                    {
                        gg.Correct = 1;
                        Moving();

                    }
                   else
                        StartCoroutine(Move());

                }
                else if (Input.GetKeyDown(KeyCode.D) && gg.ChoiceSeller == "SellerCorner2")
                {
                    gg.ChoiceSeller = null;
                    StartCoroutine(SellerMove());
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (gg.ChoiceSeller == null || gg.ChoiceSeller == "")
                    {
                        gg.Correct = 2;
                        Moving();

                    }
                    else
                        StartCoroutine(Move());
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (gg.ChoiceSeller == null || gg.ChoiceSeller == "")
                    {
                        gg.Correct = 3;
                        Moving();
                    }
                    else
                        StartCoroutine(Move());
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (gg.ChoiceSeller == null || gg.ChoiceSeller == "")
                    {
                        gg.Correct = 4;
                        Moving();
                    }
                    else
                        StartCoroutine(Move());
                }
            }
        }
    

    }
    public void Moving()
    {

        gg.Stop = true;
        switch (gg.Correct)
        {
            case 1:
                if (gg.State == "Null")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("SRoute2").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner1" && gg.ReState != "Corner2_2")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute2").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner2" && gg.ReState != "Corner4")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route1").GetComponentInChildren<Route>();
                }             
                break;
            case 2:
                if (gg.State == "Null")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("SRoute3").GetComponentInChildren<Route>();

                }
                else if (gg.State == "Corner1" && gg.ReState != "Corner2_1")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute1").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner2" && gg.ReState != "Corner1_1")
                {
                    if (!GuardPass && !gg.Guard1State && GuardOn != null)
                    {
                        Player.rotation = Quaternion.Euler(0, 180, 0);
                        GuardOn();
                        GuardPass = true;
                        
                        GuardState = true;
                    }
                    else
                    {
                        GuardPass = false;
                        GuardState = false;
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route2").GetComponentInChildren<Route>();
                        
                    }
                    
                }
                else if (gg.State == "Corner4" && gg.ReState != "Corner2")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route3").GetComponentInChildren<Route>();
                else if (gg.State == "Corner5" && gg.ReState != "Corner4_2")
                    if (!GuardPass && !gg.Guard2State && GuardOn != null)
                    {
                        Player.rotation = Quaternion.Euler(0, 180, 0);
                        GuardOn();
                        GuardPass = true;
                        
                        GuardState = true;
                    }
                    else
                    {
                        GuardPass = false;
                        GuardState = false;
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route2").GetComponentInChildren<Route>();
                        
                    }
                break;
            case 3:
                if(gg.State == "Corner2" && gg.ReState != "Corner1_2")
                {
                    if (!GuardPass && gg.Guard1State && GuardOn != null)
                    {
                        Player.rotation = Quaternion.Euler(0, 0, 0);
                        GuardOn();
                        GuardPass = true;
                        
                        GuardState = true;
                    }
                    else
                    {
                       
                        GuardPass = false;
                        GuardState = false;
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route3").GetComponentInChildren<Route>();
                        
                        Debug.Log(gg.currentRoute.chillNodeList.Count);
                    }
                    
                }
                else if(gg.State == "Corner4" && gg.ReState != "Corner5_1")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route1").GetComponentInChildren<Route>();
                }
                else if(gg.State == "Corner5" && gg.ReState != "Corner4_1")
                    if (!GuardPass && gg.Guard2State && GuardOn != null)
                    {
                        Player.rotation = Quaternion.Euler(0, 0, 0);
                        GuardOn();
                        GuardPass = true;
                        
                        GuardState = true;
                    }
                    else
                    {
                        GuardPass = false;
                        GuardState = false;
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route3").GetComponentInChildren<Route>();
                        
                    }
               
                else if(gg.State == "Corner1" && gg.ReState != "Corner5")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute3").GetComponentInChildren<Route>();
                break;
            case 4:
                if(gg.State == "Corner4" && gg.ReState != "Corner5_2")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route2").GetComponentInChildren<Route>();
                else if(gg.State == "Corner5" && gg.ReState != "Corner1")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route1").GetComponentInChildren<Route>();
                break;
            default:
                break;
        }
        if(gg.StageName != "Seller")
        gg.StageName = "null";
        if (!GuardState)
            StartCoroutine(Move());
        else
            gg.isMoving = false;
        
    }
    public IEnumerator Move()
    {
        gg.ChoiceSeller = null;
        if (gg.StageName != "Seller")
                gg.StageName = "null";
        if (gg.isMoving)
        {
           
            yield break;
            
        }
        if (!gg.currentRoute)
        {
            
            gg.Stop = false;
            gg.Correct = 0;
            yield break;
        }
        gg.isMoving = true;

        while (gg.steps > 0)
        {
            gg.Stop = true;
           
            if (gg.currentRoute.chillNodeList.Count <= gg.routePosition)
            {

                
                Stopcorutine();
                yield break;
            }
            if (gg.Break)
            {
                
                StageChange();
                yield break;
            }
            nextPos = gg.currentRoute.chillNodeList[gg.routePosition].position;
            sound.Play();
            Rotate();
            while (MoveToNextNode(nextPos)) { yield return null; }
            gg.PlayerPosition = transform.position;
            State.EnableTrue();
          
            gg.steps--;
            gg.routePosition++;
            yield return new WaitForSeconds(0.1f);
        }
        if (gg.Break &&  (gg.ChoiceSeller == "SellerCorner1" || gg.ChoiceSeller == "SellerCorner2"))
        {

            StageChange();
         
        }
        if (gg.StageName == "Seller")
            gg.StageName = "null";

        sound.Stop();
        gg.isMoving = false;
        gg.EndMoving = false;
        if (gg.StageName == "null" || gg.StageName == "Guard")
        {
            DiceCheckZoneScript.Stop = false;
            RollCanvas.On();
        }
        bool MoveToNextNode(Vector3 goal)
        {
            
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
        }
    }
    IEnumerator SellerMove()
    {
        nextPos = GameObject.Find("Seller2").transform.position;
        sound.Play();
        Player.rotation = Quaternion.Euler(0, 0, 0);
        gg.isMoving = true;
        
        while (MoveToNextNode(nextPos)) { yield return null; }
        gg.PlayerPosition = transform.position;
        sound.Stop();
        gg.steps--;
        gg.routePosition--;
        if (gg.steps == 0)
            gg.EndMoving = false;
        bool MoveToNextNode(Vector3 goal)
        {
            
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
        }
        yield return new WaitForSeconds(0.1f);
        gg.isMoving = false;
    }
    IEnumerator SellerMove1()
    {
        nextPos = GameObject.Find("Seller1").transform.position;
        Player.rotation = Quaternion.Euler(0, -90, 0);
        sound.Play();
        gg.isMoving = true;
        
        while (MoveToNextNode(nextPos)) { yield return null; }
        gg.PlayerPosition = transform.position;
        sound.Stop();
        gg.steps--;
        gg.routePosition--;
        if (gg.steps == 0)
            gg.EndMoving = false;
        bool MoveToNextNode(Vector3 goal)
        {
            
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
        }
        yield return new WaitForSeconds(0.1f);
        gg.isMoving = false;
    }
    void StageChange()
    {
        sound.Stop();
        gg.isMoving = false;
        if (gg.ChoiceSeller == "SellerCorner1" || gg.ChoiceSeller == "SellerCorner2")
        {
            gg.Stop = false;
            gg.Break = false;
        }
    }

    void Stopcorutine()
    {
        if (gg.StageName == "Seller")
            gg.StageName = "null";
        sound.Stop();
        gg.routePosition = 0;
        gg.Stop = false;
        gg.StageState = "Stage1";
        switch (gg.Correct)
        {
            case 1:
                if (gg.State == "Null")
                {
                    gg.ReState = "Corner1";
                    gg.State = "Corner5";
                }
                else if (gg.State == "Corner1")
                {
                    
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner2";
                }
                else if (gg.State == "Corner2")
                {
                    
                    gg.ReState = gg.State;
                    gg.State = "Corner4";
                }
                break;
            case 2:
                if (gg.State == "Null")
                {
                    gg.ReState = "Corner5";
                    gg.State = "Corner1";
                }
                else if (gg.State == "Corner1")
                {
                    
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner2";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner4";
                }
                else if (gg.State == "Corner4")
                {
                    gg.ReState = gg.State;
                    gg.State = "Corner2";
                }
                else if (gg.State == "Corner2")
                {
                    
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner1";
                }
                break;
            case 3:
                if (gg.State == "Corner1")
                {
                    gg.ReState = gg.State;
                    gg.State = "Corner5";

                }
                else if (gg.State == "Corner2")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner1";
                }
                else if (gg.State == "Corner4")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner5";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner4";
                }
                break;
            case 4:
                if (gg.State == "Corner4")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner5";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State;
                    gg.State = "Corner1";
                }
                break;
        }
        gg.currentRoute = null;
        gg.Correct = 0;
        gg.isMoving = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Start")
        {
            if (gg.Start)
                gg.Boss++;
            
        }
        RM.StageCheck(other);
       


    }
    void Rotate()
    {
        if (Player.transform.position.z > gg.currentRoute.chillNodeList[gg.routePosition].position.z + 1 &&
               Player.transform.position.x > gg.currentRoute.chillNodeList[gg.routePosition].position.x + 1)
            Player.rotation = Quaternion.Euler(0, 45, 0);
        else if (Player.transform.position.z < gg.currentRoute.chillNodeList[gg.routePosition].position.z - 1 &&
            Player.transform.position.x < gg.currentRoute.chillNodeList[gg.routePosition].position.x - 1)
            Player.rotation = Quaternion.Euler(0, -135, 0);
        else if (Player.transform.position.z > gg.currentRoute.chillNodeList[gg.routePosition].position.z + 1)
            Player.rotation = Quaternion.Euler(0, 180, 0);
        else if (Player.transform.position.z < gg.currentRoute.chillNodeList[gg.routePosition].position.z - 1)
            Player.rotation = Quaternion.Euler(0, 0, 0);
        else if (Player.transform.position.x > gg.currentRoute.chillNodeList[gg.routePosition].position.x + 1)
            Player.rotation = Quaternion.Euler(0, -90, 0);
        else if (Player.transform.position.x < gg.currentRoute.chillNodeList[gg.routePosition].position.x - 1)
            Player.rotation = Quaternion.Euler(0, 90, 0);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "BackCol")
        {

            if ((gg.State == "Corner4" || gg.State == "Corner1") )
            {

                Check();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "BackCol")
        {

                if(!gg.Break)
                gg.StageName = "null";
            
        }
    }
    public void Cancle()
    {
        
        gg.Break = false;
        gg.Stop = false;
        GuardPass = false;
        GuardState = false;
        
    }
    void Check()
    {

        if (!gg.PlayStop)
        {
            if ((gg.isMoving && GuardOn != null) || (gg.StageName == "Guard" && GuardOn != null))
            {
                if(gg.ReState=="Corner1_2" || gg.ReState == "Corner2_3" 
                    || gg.ReState == "Corner2_1" || gg.ReState == "Corner5_2" || (gg.ReState == "Corner5" && gg.Correct == 1))
                    Player.rotation = Quaternion.Euler(0, 180, 0);
                if(gg.ReState == "Corner1_1"||gg.ReState == "Corner5_1"
                    ||gg.ReState == "Corner2_2" || gg.ReState == "Corner2_3" || (gg.ReState == "Corner5" && gg.Correct == 2))
                    Player.rotation = Quaternion.Euler(0, 0f, 0);
                gg.StageName = "Guard";
                GuardOn();
                
            }
        }
               
    }
    public void Return()
    {
        gg.Break = false;
        //gg.Stop = false;
        GuardPass = false;
        GuardState = false;
        
                   
        if (gg.State == "Corner1")
        {
            gg.routePosition = 1;
            
            switch (gg.Correct)
            { 
                case 2:
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route2").GetComponentInChildren<Route>();
                    gg.State = "Corner2";
                    StartCoroutine(Move());
                    break;
                case 1:
                    gg.State = "Corner2";
                    gg.Correct = 3;
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route3").GetComponentInChildren<Route>();
                    StartCoroutine(Move());
                    break;
                   

            }
        }
        else if(gg.State == "Corner4")
        {
            gg.routePosition = 1;
            
            switch (gg.Correct)
            {
                case 3:
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route3").GetComponentInChildren<Route>();
                    gg.State = "Corner5";
                    StartCoroutine(Move());
                    break;
                case 4:
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route2").GetComponentInChildren<Route>();
                    gg.State = "Corner5";
                    gg.Correct = 2;
                    StartCoroutine(Move());
                    break;
            
            
            }
            
        }
    }
    
}
