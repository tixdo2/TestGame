using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastle : Castle
{
    /*
    public void CreateUnitMilitia()
    {
        Army.CreateUnit(TypeUnit.Militia);
        Resources.Gold -= Army.Militias.Cost;
    }

    public void CreateUnitShooter()
    {
        Army.CreateUnit(TypeUnit.Shooter);
        Resources.Gold -= Army.Shooters.Cost;
    }

    public void CreateUnitInfantry()
    {
        Army.CreateUnit(TypeUnit.Infantry);
        Resources.Gold -= Army.Infantryes.Cost;
    }

    public void CreateUnitCavalry()
    {
        Army.CreateUnit(TypeUnit.Cavalry);
        Resources.Gold -= Army.Infantryes.Cost;
    }

    public void CreateTroop(List<TypeUnit> typeUnit, List<int> amount)
    {
        Vector3 castlePosition = transform.position;
        Vector3 combatPosition = new Vector3(castlePosition.x - 5, 0, castlePosition.z); 
        var combatGameObject = Instantiate(CombatUnitPrefab, combatPosition, Quaternion.identity);
        var combatUnits = combatGameObject.GetComponent<СombatUnit>();
        combatUnits.Init(Army.CreateTroop(typeUnit, amount));
    }
    */
}
