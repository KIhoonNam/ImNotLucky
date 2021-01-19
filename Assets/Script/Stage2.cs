using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage2 : MonoBehaviour
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
    
    private void Awake()
    {
        gg = FindObjectOfType<GameManager>();

        sound = GetComponent<SoundScript>();
     
        
       
        
     
    }
    private void Start()
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
        gg.BattleState = null;
        gg.StageIndex = "Stage2";
        
        gg.Break = false;
        gg.currentRoute = null;
        if(gg.ChoiceSeller == "Seller" && gg.steps > 0)
        {
            StartCoroutine(SellerMove());
        }
        else if (GuardOn == null && gg.StageName != "Guard" && gg.ChoiceSeller != "Seller")
            Moving();
        if ((gg.Start == false || gg.ChoiceSeller == "Seller") && gg.steps<=0)
        {
            RollCanvas.On();
            DiceCheckZoneScript.Stop = false;
        }
    }
    void DiceSteps()
    {
        DiceStat.DiceS -= DiceSteps;

        gg.steps = DiceCheckZoneScript.diceNumber;
        Debug.Log("Dice Roll" + gg.steps);
        Result(gg.steps,null);
        if (gg.ChoiceSeller == "Seller")
        {
            StartCoroutine(SellerMove());
        }
        else if (gg.Stop == true && gg.ChoiceSeller == null)
        {

            StartCoroutine(Move());

        }
    }
    IEnumerator SellerMove()
    {
        nextPos = GameObject.Find("2-13").transform.position;
        sound.Play();
        Player.rotation = Quaternion.Euler(0, 0, 0);
        gg.isMoving = true;
        while (MoveToNextNode(nextPos)) { yield return null; }
        gg.PlayerPosition = transform.position;
        sound.Stop();
        gg.steps--;
        gg.isMoving = false;
        gg.ReState = null;
        gg.routePosition = 0;
        gg.currentRoute = null;
        //gg.routePosition--;
        if (gg.steps <= 0)
        {
            DiceCheckZoneScript.Stop = false;
            RollCanvas.On();
            gg.EndMoving = false;

            



        }
        else
            gg.Stop = false;
        bool MoveToNextNode(Vector3 goal)
        {

            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
        }
        yield return new WaitForSeconds(0.1f);
       // gg.ChoiceSeller = null;
        sound.Stop();
       
        gg.StageState = "Stage2";
       
        
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
    void Update()
    {


        if (Time.timeScale == 0)
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

           
        //}

        //if (Input.GetKeyDown(KeyCode.W) && gg.ChoiceSeller)
        //{
        //    gg.ChoiceSeller = false;
        //    StartCoroutine(SellerMove());
        //}

        if (!GuardCanvas.enabled)
        {
            if (!gg.Stop && gg.Start && gg.steps>0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    gg.Correct = 1;
                    Moving();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    gg.Correct = 2;
                    Moving();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    gg.Correct = 3;
                    Moving();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    gg.Correct = 4;
                    Moving();
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
                if (gg.State == "Null" && gg.ReState != "Corner4_1")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("SRoute2").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner1" && gg.ReState != "Corner2_3")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute2").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner4" && gg.ReState != "Corner5_1")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route2").GetComponentInChildren<Route>();
                else if (gg.State == "Corner5" && gg.ReState != "Corner4_2")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route2").GetComponentInChildren<Route>();
              
                break;
            case 2:
                if (gg.State == "Null" && gg.ReState != "Corner1_2")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("SRoute1").GetComponentInChildren<Route>();
                }
                else if (gg.State == "Corner1" && gg.ChoiceSeller != "Seller")
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
                        gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute1").GetComponentInChildren<Route>();

                    }
                }
                else if(gg.State == "Corner2" && gg.ReState != "Corner5_3")
                {
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
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route2").GetComponentInChildren<Route>();

                    }
                   // gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route2").GetComponentInChildren<Route>();
                }
                else if(gg.State == "Corner4" && gg.ReState != "Corner2_2")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route3").GetComponentInChildren<Route>();
              
                break;
            case 3:
                if (gg.State == "Corner2" && gg.ReState != "Corner4_3")
                {
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
                        gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route3").GetComponentInChildren<Route>();

                    }
                  //  gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route3").GetComponentInChildren<Route>();
                }
                else if(gg.State == "Corner4" && gg.ReState != "Null_2")
                {
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C3Route1").GetComponentInChildren<Route>();
                }
                else if(gg.State == "Corner5" && gg.ReState != "Corner2_1")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route3").GetComponentInChildren<Route>();
                else if(gg.State == "Corner1" && gg.ReState != "Null_1")
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
                        gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute3").GetComponentInChildren<Route>();

                        
                    }

                }
               // gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute3").GetComponentInChildren<Route>();
                break;
            case 4:

                if (gg.State == "Null" && gg.ReState != "Corner5_2")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("SRoute3").GetComponentInChildren<Route>();
                else if(gg.State == "Corner2" && gg.ReState != "Corner1_1")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route1").GetComponentInChildren<Route>();

                else if(gg.State == "Corner5" && gg.ReState != "Null_3")
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C4Route1").GetComponentInChildren<Route>();
                break;
            default:
                break;
        }
       
        gg.StageName = "null";
        if (!GuardState)
        {
            gg.ChoiceSeller = null;
            StartCoroutine(Move());
        }
        else

            gg.isMoving = false;
    }
    public IEnumerator Move()
    {

       
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
        sound.Stop();
        if (gg.StageName == "null" || gg.StageName == "Guard")
        {
            DiceCheckZoneScript.Stop = false;
            RollCanvas.On();
        }
        gg.isMoving = false;
        gg.EndMoving = false;
        bool MoveToNextNode(Vector3 goal)
        {
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 6f * Time.deltaTime));
        }
    }
    void Stopcorutine()
    {
        sound.Stop();
        gg.routePosition = 0;
        gg.Stop = false;
        gg.StageState = "Stage2";

        switch (gg.Correct)
        {
            case 1:
                if (gg.State == "Null")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner4";
                }
                else if (gg.State == "Corner1")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner2";
                }
                else if (gg.State == "Corner4")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner5";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner4";
                }
                break;
            case 2:
                if (gg.State == "Null")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner1";
                }
                else if (gg.State == "Corner1" && gg.ReState == "Corner2_3")
                {
                   
                    gg.State = "Corner1";
                }
                else if (gg.State == "Corner1" && gg.ReState == "Null_1")
                {
                  
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
                    gg.ReState = gg.State + "_3";
                    gg.State = "Corner2";
                }
                else if (gg.State == "Corner2")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Corner5";
                }
                break;
            case 3:
                if (gg.State == "Corner1")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Null";

                }
                else if (gg.State == "Corner2")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Corner4";
                }
                else if (gg.State == "Corner4")
                {
                    gg.ReState = gg.State + "_1";
                    gg.State = "Null";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State + "_3";
                    gg.State = "Corner2";
                }
                break;
            case 4:
                
                if (gg.State == "Null")
                {
                    gg.ReState = gg.State + "_3";
                    gg.State = "Corner5";
                }
                else if (gg.State == "Corner5")
                {
                    gg.ReState = gg.State + "_2";
                    gg.State = "Null";
                }
                else if (gg.State == "Corner2")
                {
                    gg.ReState = gg.State + "_3";
                    gg.State = "Corner1";
                }
                break;
        }

        gg.currentRoute = null;
        gg.Correct = 0;
        gg.isMoving = false;
    }
 
    void Rotate()
    {
        if (Player.transform.position.z > gg.currentRoute.chillNodeList[gg.routePosition].position.z + 0.2 &&
             Player.transform.position.x > gg.currentRoute.chillNodeList[gg.routePosition].position.x + 0.2)
            Player.rotation = Quaternion.Euler(0, -135, 0);
        else if (Player.transform.position.z < gg.currentRoute.chillNodeList[gg.routePosition].position.z - 0.2 &&
            Player.transform.position.x < gg.currentRoute.chillNodeList[gg.routePosition].position.x - 0.2)
            Player.rotation = Quaternion.Euler(0, 45, 0);
        else if (Player.transform.position.z > gg.currentRoute.chillNodeList[gg.routePosition].position.z + 1)
            Player.rotation = Quaternion.Euler(0, 180, 0);
        else if (Player.transform.position.z < gg.currentRoute.chillNodeList[gg.routePosition].position.z - 1)
            Player.rotation = Quaternion.Euler(0, 0, 0);
        else if (Player.transform.position.x > gg.currentRoute.chillNodeList[gg.routePosition].position.x + 1)
            Player.rotation = Quaternion.Euler(0, -90, 0);
        else if (Player.transform.position.x < gg.currentRoute.chillNodeList[gg.routePosition].position.x - 1)
            Player.rotation = Quaternion.Euler(0, 90, 0);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Start")
        {
            if (gg.Start)
                gg.Boss++;
        }
        RM.StageCheck(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "BackCol")
        {

            if (gg.State == "Corner5" || gg.State == "Corner4" || gg.State == "Null")
                Check();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "BackCol")
        {

            if (!gg.Break)
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
                if (gg.State == "Null" || gg.State == "Corner4")
                    Player.rotation = Quaternion.Euler(0, 180, 0);
                if ( gg.State == "Corner5")
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


        if (gg.State == "Null")
        {
            gg.routePosition = 1;

            switch (gg.Correct)
            {
                
                case 2:
                   gg.currentRoute = GameObject.FindGameObjectWithTag("CRoute3").GetComponentInChildren<Route>();
                        gg.State = "Corner1";
                        gg.Correct = 3;
                        StartCoroutine(Move());
                    
                    break;
             


            }
        }
        else if (gg.State == "Corner5")
        {
            gg.routePosition = 1;

            switch (gg.Correct)
            {
                case 3:
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route2").GetComponentInChildren<Route>();
                    gg.State = "Corner2";
                    gg.Correct = 2;
                    StartCoroutine(Move());
                    break;
            

            }

        }
        else if (gg.State == "Corner4")
        {
            gg.routePosition = 1;

            switch (gg.Correct)
            {
                case 2:
                    gg.currentRoute = GameObject.FindGameObjectWithTag("C2Route3").GetComponentInChildren<Route>();
                    gg.State = "Corner2";
                    gg.Correct = 3;
                    StartCoroutine(Move());
                    break;


            }

        }
    }
}
