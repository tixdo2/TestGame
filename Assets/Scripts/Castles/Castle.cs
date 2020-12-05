using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class Castle : MonoBehaviour
{
    public Resource Resources;
    public int MaxPopulation;
    public Army Army;
    public GameObject CombatUnitPrefab;
    
    public TownHall TownHall;
    public ResidentBuildings ResidentBildings;
    public Church Church;
    public Walls Walls;
    public Barracks Barracks;
    

    public int CastleID;

    private List<СombatUnit> _combatUnits;

    
    
    private void Awake()
    {
        MaxPopulation     = 5000;
        Resources         = new Resource(500, 100);
        
        TownHall          = new TownHall();
        ResidentBildings  = new ResidentBuildings();
        Church            = new Church();
        Walls             = new Walls();
        Barracks          = new Barracks();
        Army              = new Army();
        _combatUnits       = new List<СombatUnit>();
        
    }

    public virtual void StartTurn()
    {
        TownHall.StartTurn(this);
        ResidentBildings.StartTurn(this);
        Church.StartTurn(this);
        Walls.StartTurn(this);
        Barracks.StartTurn(this);
        Army.StartTurn();

        foreach (var units in _combatUnits)
        {
            units.StartTurn();
        }
    }

    public virtual void DieCombatUnit(СombatUnit combatUnit)
    {
        _combatUnits.Remove(combatUnit);
    }

    public virtual void UpgradeTownHall()
    {
        TownHall.UpgradeBuilding(this);
    }
    
    public virtual void UpgradeResidentBuildings()
    {
        ResidentBildings.UpgradeBuilding(this);
    }
    
    public virtual void UpgradeChurch()
    {
        Church.UpgradeBuilding(this);
    }
    
    public virtual void UpgradeWalls()
    {
        Walls.UpgradeBuilding(this);
    }
    
    public virtual void UpgradeBarracks()
    {
        Barracks.UpgradeBuilding(this);
    }
    
    public virtual void CreateUnitMilitia()
    {
        Army.CreateUnit(TypeUnit.Militia);
        Resources.Gold -= Army.Militias.Cost;
    }

    public virtual void CreateUnitShooter()
    {
        Army.CreateUnit(TypeUnit.Shooter);
        Resources.Gold -= Army.Shooters.Cost;
    }

    public virtual void CreateUnitInfantry()
    {
        Army.CreateUnit(TypeUnit.Infantry);
        Resources.Gold -= Army.Infantryes.Cost;
    }

    public virtual void CreateUnitCavalry()
    {
        Army.CreateUnit(TypeUnit.Cavalry);
        Resources.Gold -= Army.Infantryes.Cost;
    }

    public virtual void CreateTroop(List<TypeUnit> typeUnit, List<int> amount)
    {
        Vector3 castlePosition = transform.position;
        Vector3 combatPosition = new Vector3(castlePosition.x - 5, 0, castlePosition.z); 
        var combatGameObject = Instantiate(CombatUnitPrefab, combatPosition, Quaternion.identity);
        var combatUnits = combatGameObject.GetComponent<СombatUnit>();
        combatUnits.Units = Army.CreateTroop(typeUnit, amount);
        combatUnits.Init(this);
        _combatUnits.Add(combatUnits);
    }

    public virtual void Defense(СombatUnit enemyCombatUnit)
    {
        var castelCombatUnit = InitTroopForDefense();
        
        enemyCombatUnit.Battle(castelCombatUnit);
    }

    private СombatUnit InitTroopForDefense()
    {
        var combatUnit = gameObject.AddComponent<СombatUnit>();
        combatUnit.Init(this);
        return combatUnit;
    }
    
    
    
}
[Serializable]
public struct Resource
{
    public int Gold;
    public int Population;

    public Resource(int gold, int population)
    {
        Gold = gold;
        Population = population;
    }
}





