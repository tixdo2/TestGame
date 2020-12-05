using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public delegate void TurnHandler(int castleID);

    public event TurnHandler StartTurnEvent;

    public int LastID;


    public void Start()
    {
       // 
        //Debug.Log(StartTurnEvent);
    }

    public void StartGame()
    {
        StartTurnEvent?.Invoke(0);
    }
    

    public void EndTurn(int castleID)
    {
        
        int playerID = castleID + 1;
        if (castleID == LastID)
            playerID = 0;
        //Debug.Log("Turn is player - " + playerID);
        
        StartTurnEvent?.Invoke(playerID);
        
    }
    
}
