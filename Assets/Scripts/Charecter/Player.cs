using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private UIManager ui;
    [SerializeField] private ArmyUI uiArmy;
    public void StartGame()
    {
        turn.StartGame();
    }
    public override void StartTurn(int castleID)
    {
        if (castle.CastleID != castleID)
            return;
        Debug.Log("PlayerTurn");
        castle.StartTurn();
        _isMyTurn = true;
        ui.StartPlayerTurn();
        uiArmy.StartPlayerTurn();
    }
    public override void EndTurn()
    {

        base.EndTurn();
    }
}
