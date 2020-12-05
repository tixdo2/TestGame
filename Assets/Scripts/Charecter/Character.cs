using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Castle castle;
    public delegate void TurnHandlerCharacter(int castleID);
    public event TurnHandlerCharacter EndTurnEvent;

    public Turn turn;
    protected bool _isMyTurn;


    public virtual void InitEvents()
    {
        turn.StartTurnEvent += StartTurn;
        
        EndTurnEvent += turn.EndTurn;
    }

    public virtual void StartTurn(int castleID)
    {
        //Debug.Log("Start Turn - " + castleID);
    }

    public virtual void EndTurn()
    {
        if(_isMyTurn)
        {
            EndTurnEvent?.Invoke(castle.CastleID);
            _isMyTurn = false;
        }
    }
    
    
    
}
