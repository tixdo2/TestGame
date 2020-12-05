using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Character
{
    
    public override void StartTurn(int castleID)
    {
        if (castle.CastleID != castleID)
            return;
        castle.StartTurn();
        _isMyTurn = true;
        StartCoroutine(Turn());
        //EndTurn();
    }

    private IEnumerator Turn()
    {
        yield return null;
        EndTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
    }
    
    
}
