using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteManager : MonoBehaviour
{
    GameManager gg;
    NodeState Box;
    SoundScript sound;
    Canvas Guard;
    CameraControll Camera;

    public void Start()
    {
        gg = FindObjectOfType<GameManager>();
        Box = FindObjectOfType<NodeState>();
        sound = GetComponent<SoundScript>();
        Camera = FindObjectOfType<CameraControll>();
        sound.Play();
    }


    public void Seller()
    {
        gg.StageName = "Seller";
        gg.StageState = "Seller";
        gg.SellerCome = true;
        // gg.routePosition;

        gg.SaveGame();

        LoadSceneControl.LoadingScene("Seller");
    }
    public void Invoke()
    {
        gg.StageState = "Stage";
       
        gg.SaveGame();

        LoadSceneControl.LoadingScene("Stage Scene");
    }
    public void StageCheck(Collider col)
    {
        if (Box.chillObjects[0].enabled)
        {
            if (gg.StageIndex == "Stage1")
            {
                if ((col.gameObject.name == "SellerCorner1" || col.gameObject.name == "SellerCorner2") && gg.SellerCome == false)
                {
                    gg.ChoiceSeller = col.gameObject.name;
                    if ((gg.steps >= 1 && gg.StageName != "Guard") || (gg.steps > 0 && gg.StageName == "Guard"))
                        gg.Break = true;
                    else
                        gg.Stop = false;



                }
                else if ((col.gameObject.name == "SellerCorner1" || col.gameObject.name == "SellerCorner2") && gg.SellerCome == true)
                    gg.SellerCome = false;
                if (col.gameObject.name == "Seller2" || col.gameObject.name == "Seller1")
                {
                    gg.Fade.Fade();
                    Invoke("Seller", 1f);
                }
                if (col.gameObject.name == "Start")
                {
                    if (gg.Boss >= 3)
                    {
                        gg.BattleState = "Uniq";
                        gg.StageName = "Stage";
                        gg.State = "Boss1";
                        gg.Monster1 = "MiniGolem";
                        gg.Monster2 = null;
                        gg.Monster3 = null;
                        gg.Break = true;
                        gg.Boss = 0;
                        gg.Fade.Fade();
                        Invoke("Invoke", 1f);
                    }
                }

                if (col.gameObject.name == "1-1")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = null;
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }

                if (col.gameObject.name == "1-4")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = null;
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-10")
                {
                    gg.BattleState = "Uniq";
                    gg.StageName = "Stage";
                    gg.Monster1 = "MushroomRed";
                    gg.Monster2 = "MushroomBlue";
                    gg.Monster3 = "MushroomGreen";
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-11")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = null;
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-13")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Rhino_PBR";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-17")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-20")
                {

                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-26")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Rhino_PBR";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-27")
                {
                    gg.BattleState = "Uniq";
                    gg.StageName = "Stage";
                    gg.Monster1 = "MushroomRed";
                    gg.Monster2 = "MushroomBlue";
                    gg.Monster3 = "MushroomGreen";

                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);

                }
                if (col.gameObject.name == "1-29")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-31")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;
                    gg.Fade.Fade();
                    Invoke("Invoke", 1f);
                }
                if (col.gameObject.name == "1-34")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
            }
            else if (gg.StageIndex == "Stage2")
            {
                if (col.gameObject.name == "Seller")
                {

                    gg.ChoiceSeller = "Seller";
                    Invoke("Seller", 1f);
                }
                if (col.gameObject.name == "2STGAE_H")
                {
                    if (gg.Boss == 3)
                    {
                        gg.BattleState = "Uniq";
                        gg.Monster1 = "GruntBoss";
                        gg.Monster2 = null;
                        gg.Monster3 = null;
                        gg.Break = true;

                        Invoke("Invoke", 1f);
                        gg.Fade.Fade();
                    }
                }
                if (col.gameObject.name == "2-2")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Rhino_PBR";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-4")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-12")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Spider";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }

                if (col.gameObject.name == "2-14")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = null;
                    gg.Monster3 = "Skeleton";
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-15")
                {
                    Guard = GameObject.FindGameObjectWithTag("Cnavas").GetComponent<Canvas>();
                    if (Guard.enabled)
                    {
                        Guard.enabled = false;
                        Camera.stop = false;
                    }
                    gg.BattleState = "Uniq";
                    
                    gg.Monster1 = "RockGolem";
                    gg.Monster2 = "RockGolem";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-16")
                {
                    gg.StageName = "null";
                    gg.StageName = "Stage";
                    gg.Monster1 = "Skeleton";
                    gg.Monster2 = "Skeleton";
                    gg.Monster3 = "Spider";
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-20")
                {

                    gg.StageName = "Null";
                    gg.Monster1 = "Spider";
                    gg.Monster2 = "Spider";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-22")
                {
                    gg.BattleState = "Uniq";
                    gg.StageName = "Stage";
                    gg.Monster1 = "RockGolem";
                    gg.Monster2 = "Grunt";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-23")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Rhino_PBR";
                    gg.Monster2 = "Rhino_PBR";
                    gg.Monster3 = "Rhino_PBR";
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
                if (col.gameObject.name == "2-26")
                {
                    gg.StageName = "Stage";
                    gg.Monster1 = "Spider";
                    gg.Monster2 = "Spider";
                    gg.Monster3 = null;
                    gg.Break = true;

                    Invoke("Invoke", 1f);
                    gg.Fade.Fade();
                }
            }
        }
    }

}

