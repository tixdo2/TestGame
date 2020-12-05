using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//строения, которые может улучшать игрок

public class Building
{
    public int Level = 1;

    internal int CostUpgrade => _startCost * Level;

    
    protected int _startCost;
    protected int _maxLevel;
    public virtual void StartTurn(Castle castle)
    {
        
    }

    public virtual void UpgradeBuilding(Castle castle)
    {
        
    }
    
}

public class TownHall : Building
{
    public new int Level = 1;
    internal new int CostUpgrade => _startCost * Level;
    
    private int _tax=10;
    private new int _startCost = 200;
    private new int _maxLevel = 10;
    
    public override void StartTurn(Castle castle)
    {
        castle.Resources.Gold += castle.Resources.Population * _tax / 100;
    }

    public override void UpgradeBuilding(Castle castle)
    {

        UpLevel(castle);
        if(_tax < 28)
            _tax += 2;
        
    }
    private void UpLevel(Castle castle)
    {
        if (Level >= _maxLevel)
            return;
        if (castle.Resources.Gold < CostUpgrade)
            return;
        castle.Resources.Gold -= CostUpgrade;
        Level++;
    }
}

public class ResidentBuildings : Building
{
    public int Level = 1;
    internal new int CostUpgrade => _startCost * Level;
    private int _incomingPopulation => 10 * Level;
    private int _rasingPopultaion => 200 * Level;

    private int _startCost = 150;
    private int _maxLevel = 25;

    public override void StartTurn(Castle castle)
    {
        if (castle.Resources.Population < castle.MaxPopulation)
            castle.Resources.Population += _incomingPopulation;

        if (castle.Resources.Population > castle.MaxPopulation)
            castle.Resources.Population = castle.MaxPopulation;
    }

    public override void UpgradeBuilding(Castle castle)
    {
        UpLevel(castle);
        
        castle.MaxPopulation += _rasingPopultaion;
    }
    
    private void UpLevel(Castle castle)
    {
        if (Level >= _maxLevel)
            return;
        if (castle.Resources.Gold < CostUpgrade)
            return;
        castle.Resources.Gold -= CostUpgrade;
        Level++;
    }

}

public class Walls : Building
{
    public int Level = 1;
    internal new int CostUpgrade => _startCost * Level;
    private int _maxLevel = 10;
    private int _startCost = 500;
    
    
    public override void StartTurn(Castle castle)
    {

    }

    public override void UpgradeBuilding(Castle castle)
    {
        UpLevel(castle);



        if (Level % 2 == 0)
        {
            castle.Army.Defense += 1;
        }
    }
    
    private void UpLevel(Castle castle)
    {
        if (Level >= _maxLevel)
            return;
        if (castle.Resources.Gold < CostUpgrade)
            return;
        castle.Resources.Gold -= CostUpgrade;
        Level++;
    }
}

public class Church : Building
{
    public int Level = 1;
    internal new int CostUpgrade => _startCost * Level;
    private int _maxLevel = 10;
    private int _startCost = 750;
    public override void StartTurn(Castle castle)
    {

    }

    public override void UpgradeBuilding(Castle castle)
    {
        UpLevel(castle);

        if (Level % 3 == 0)
            castle.Army.Attack += 1;
        if (Level % 2 == 0)
            castle.Army.AttackOnDefense += 1;
    }
    
    private void UpLevel(Castle castle)
    {
        if (Level >= _maxLevel)
            return;
        if (castle.Resources.Gold < CostUpgrade)
            return;
        castle.Resources.Gold -= CostUpgrade;
        Level++;
    }
}

public class Barracks : Building
{
    public int Level = 1;
    public bool CanBuyInfantry => Level >= 4;
    public bool CanBuyСavalry => Level >= 7;
    internal new int CostUpgrade => _startCost * Level;

    //скорость найма
    
    private int _maxLevel = 10;
    private int _startCost = 300;
    
    
    
    public override void StartTurn(Castle castle)
    {
        
    }

    public override void UpgradeBuilding(Castle castle)
    {
        UpLevel(castle);

        
        if (Level == 2 || Level == 3)
            castle.Army.Shooters.PreparationTime -= 1;
        
        if (Level == 5 || Level == 6)
            castle.Army.Infantryes.PreparationTime -= 1;
        
        if (Level == 8 || Level == 9)
            castle.Army.Cavalryes.PreparationTime -= 1;
    }
    
    private void UpLevel(Castle castle)
    {
        if (Level >= _maxLevel)
            return;
        if (castle.Resources.Gold < CostUpgrade)
            return;
        castle.Resources.Gold -= CostUpgrade;
        Level++;
    }
    
}