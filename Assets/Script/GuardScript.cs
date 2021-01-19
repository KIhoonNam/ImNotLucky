using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardScript : MonoBehaviour
{
    
    Vector3 CheckPointPos;
    public Canvas canvas;
    Animator anim;
    GameManager gg;
    InfoScript info;
    Character Play;
    CameraControll Camera;
    Stage2 Stage2;

    SoundScript sound;
    private void Awake()
    {
        
        gg = FindObjectOfType<GameManager>();
        anim = GetComponentInChildren<Animator>();
        Play = FindObjectOfType<Character>();
        sound = GetComponent<SoundScript>();
        Stage2 = FindObjectOfType<Stage2>();
        info = FindObjectOfType<InfoScript>();
        Camera = FindObjectOfType<CameraControll>();

    }
    private void Start()
    {
        GuardMove.Move += Moving;
        
        GuardStop.Stop += Stoping;
        LoadGuard();
    }
    private void OnDestroy()
    {
        Character.GuardOn -= this.PlayerJoin;
        Stage2.GuardOn -= this.PlayerJoin;
        GuardMove.Move -= Moving;
        InfoScript.Stop -= Action;
        InfoScript.CheckPointer -= MoveCheckPoint;
        GuardStop.Stop -= Stoping;
    }
    void PlayerJoin()
    {
        if (gg.Break)
        {

        }
        else
        {
            gg.Break = true;
            canvas = GameObject.FindGameObjectWithTag("Cnavas").GetComponent<Canvas>();
            canvas.enabled = true;
            Camera.stop = true;
            sound.Play();

            if (gg.StageName == "Guard")
            {
                this.transform.Rotate(new Vector3(0, 180, 0));

            }
        }
        
        
    }

    void Action()
    {
       
        if (!gg.PlayStop)
        {
            Invoke("CameraStop", 1f);
            this.anim.SetBool("IsAttack", true);
            if (gg.StageName == "Guard" && this.anim.GetBool("IsAttack"))
            {
                Invoke("Roate", 1.5f);
                

            }
            
                
            gg.PlayStop = true;
        }


    }
    void CameraStop()
    {
        Camera.stop = false;
    }
    void Roate()
    {
        this.transform.Rotate(new Vector3(0, 180, 0));
    }
    void Stoping()
    {
        
        this.anim.SetBool("IsAttack", false);
        gg.PlayStop = false;
    }
    void LoadGuard()
    {
        if(this.name == "Guard" && gg.Guard1State)
        {
            this.transform.position = this.GetComponentInChildren<Transform>().Find("Checkpoint1").position;
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
        if(this.name == "Guard1"&&gg.Guard2State)
        {
            this.transform.position = this.GetComponentInChildren<Transform>().Find("Checkpoint1").position;
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void MoveCheckPoint()
    {
        

        if (!gg.PlayStop)
        {
            gg.PlayStop = true;
            if (this.name == "Guard")
            { 
                if (!gg.Guard1State)
                {


                    anim.SetBool("IsMoving", true);
                    gg.Guard1State = true;
                }
                else if (gg.Guard1State)
                {


                    anim.SetBool("IsMoving", true);
                    gg.Guard1State = false;
                }
            }
            if (this.name == "Guard1")
            {
                if (!gg.Guard2State)
                {


                    anim.SetBool("IsMoving", true);
                    gg.Guard2State = true;
                }
                else if (gg.Guard2State)
                {


                    anim.SetBool("IsMoving", true);
                    gg.Guard2State = false;
                }
            }

        }

    }

    void Moving()
    {
        Camera.stop = false;
        if (this.name == "Guard" && this.anim.GetBool("IsMoving"))
        {
            if (!gg.Guard1State)
            {
                if(gg.StageName == "Guard")
                {
                    this.transform.Rotate(new Vector3(0, 180, 0));
                }
                CheckPointPos = this.GetComponentInChildren<Transform>().Find("Checkpoint1").position;
                StartCoroutine(Move());
                
            }
            else if (gg.Guard1State)
            {
                if (gg.StageName == "Guard")
                {
                    this.transform.Rotate(new Vector3(0, 180, 0));
                }
                CheckPointPos = this.GetComponentInChildren<Transform>().Find("Checkpoint").position;
                StartCoroutine(Reutrn());
                
            }
        }
        if (this.name == "Guard1" && this.anim.GetBool("IsMoving"))
        {
            if (!gg.Guard2State)
            {
                if (gg.StageName == "Guard")
                {
                    this.transform.Rotate(new Vector3(0, 180, 0));
                }
                CheckPointPos = this.GetComponentInChildren<Transform>().Find("Checkpoint1").position;
                StartCoroutine(Move());
              
            }
            else if (gg.Guard2State)
            {
                if (gg.StageName == "Guard")
                {
                    this.transform.Rotate(new Vector3(0, 180, 0));
                }
                CheckPointPos = this.GetComponentInChildren<Transform>().Find("Checkpoint").position;
                StartCoroutine(Reutrn());
               
            }
        }
    }
    IEnumerator Reutrn()
    {
        while (MoveToNextNode(CheckPointPos)) { yield return null; }
        bool MoveToNextNode(Vector3 goal)
        {
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, Time.deltaTime));
        }
        anim.SetBool("IsMoving", false);
        this.transform.Rotate(new Vector3(0, 180, 0));
        gg.PlayStop = false;
        gg.Break = false;
        if (Play != null)
            Play.Moving();
        if (Stage2 != null)
            Stage2.Moving();
        
    }
    IEnumerator Move()
    {
        while (MoveToNextNode(CheckPointPos)) { yield return null; }
        bool MoveToNextNode(Vector3 goal)
        {
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, Time.deltaTime));
        }
        anim.SetBool("IsMoving", false);
        this.transform.Rotate(new Vector3(0, 180, 0));
        gg.PlayStop = false;
        gg.Break = false;

        if (Play != null)
            Play.Moving();
        if (Stage2 != null)
            Stage2.Moving();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Enter");
            Character.GuardOn += this.PlayerJoin;
            InfoScript.CheckPointer += MoveCheckPoint;
            Stage2.GuardOn += this.PlayerJoin;
            InfoScript.Stop += Action;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
            Character.GuardOn -= this.PlayerJoin;
            Stage2.GuardOn -= this.PlayerJoin;
            InfoScript.CheckPointer -= MoveCheckPoint;
            InfoScript.Stop -= Action;
        }
    }
}
