using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : Castle
{
    public override void StartTurn()
    {        
        Debug.Log("Bot " + CastleID);

        //base.StartTurn();
    }
}
