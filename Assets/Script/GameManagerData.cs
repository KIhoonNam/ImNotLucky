using System.Collections;
using UnityEngine;
[System.Serializable]
public class GameManagerData
{
   
    public int steps { get; set; }
    public int Correct { get; set; }
    public int Boss { get; set; }
    public bool Start { get; set; }
    public bool Stop { get; set; }
    public bool isMoving { get; set; }
    public bool EndMoving { get; set; }
    public int routePosition { get; set; }
    public string ChoiceSeller { get; set; }
    public float PlayerPositionx { get; set; }
    public float PlayerPositiony { get; set; }
    public float PlayerPositionz { get; set; }
    public bool SellerCome { get; set; }
    public string State { get; set; }
    public string ReState { get; set; }
   // public Route currentRoute { get; set; }
    public bool Break { get; set; }
    public string StageIndex { get; set; }
    public string StageName { get; set; }
    public string Monster1 { get; set; }
    public string Monster2 { get; set; }
    public string Monster3 { get; set; }
    public string StageState { get; set; }

    public bool Guard1State { get; set; }
    public bool Guard2State { get; set; }
    public string BattleState { get; set; }
    public GameManagerData(GameManager Manager)
    {
        steps = Manager.steps;
        Correct = Manager.Correct;
        Boss = Manager.Boss;
        Start = Manager.Start;
        Stop = Manager.Stop;
        isMoving = Manager.isMoving;
        EndMoving = Manager.EndMoving;
        routePosition = Manager.routePosition;
        PlayerPositionx = Manager.PlayerPosition.x;
        PlayerPositiony = Manager.PlayerPosition.y;
        PlayerPositionz = Manager.PlayerPosition.z;
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



    }
}
