using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Army
{
    public int Attack, AttackOnDefense, Defense;

    public Militia   Militias;
    public Shooter   Shooters;
    public Infantry  Infantryes;
    public Cavalry   Cavalryes;

    public List<Troop> Troops;

    public Army()
    {
        Militias    = new Militia();
        Shooters    = new Shooter();
        Infantryes  = new Infantry();
        Cavalryes   = new Cavalry();
        Troops      = new List<Troop>();
    }

    public void StartTurn()
    {
        Militias.StartTurn();
        Shooters.StartTurn();
        Infantryes.StartTurn();
        Cavalryes.StartTurn();
    }

    public int ReadyUnits(TypeUnit typeUnit)
    {
        int amount = 0;
        
        switch (typeUnit)
        {
            case TypeUnit.Militia:  amount = Militias.ReadUnits.Count;   break;
            case TypeUnit.Shooter:  amount = Shooters.ReadUnits.Count;   break;
            case TypeUnit.Infantry: amount = Infantryes.ReadUnits.Count; break;
            case TypeUnit.Cavalry:  amount = Cavalryes.ReadUnits.Count;  break;
        }
        return amount;
    }

    
    public void CreateUnit(TypeUnit typeUnit)
    {
        switch (typeUnit)
        {
            case TypeUnit.Militia:  Militias.CreateUnit();   break;
            case TypeUnit.Shooter:  Shooters.CreateUnit();   break;
            case TypeUnit.Infantry: Infantryes.CreateUnit(); break;
            case TypeUnit.Cavalry:  Cavalryes.CreateUnit();  break;
        }
    }

    //создание похода из множесва типов юнитов
    public Troop CreateTroop(List<TypeUnit> typeUnit, List<int> amount)
    {
        List<Unit> troopUnits = new List<Unit>(typeUnit.Count);
        
        for (int i = 0; i < typeUnit.Count; i++)
        {
            switch (typeUnit[i])
            {
                case TypeUnit.Militia:
                    for(int j = 0; j< amount[i]; j++)
                    {
                        troopUnits.Add(Militias.ReadUnits[0]);
                        //Militias.ReadUnits.Remove(Militias.ReadUnits[0]);
                    }

                    for (int j = 0; j < amount[i]; j++)
                    {
                        //troopUnits.Add(Militias.ReadUnits[0]);
                        Militias.ReadUnits.Remove(Militias.ReadUnits[0]);
                        Militias.Amount--;
                    }

                    break;
                case TypeUnit.Shooter:
                    for(int j = 0; j< amount[i]; j++)
                    {
                        troopUnits.Add(Shooters.ReadUnits[0]);
                        //Shooters.ReadUnits.Remove(Militias.ReadUnits[0]);
                    }
                    for(int j = 0; j< amount[i]; j++)
                    {
                   
                        //troopUnits.Add(Shooters.ReadUnits[0]);
                        Shooters.ReadUnits.Remove(Shooters.ReadUnits[0]);
                        Shooters.Amount--;
                    }
                    break;
                case TypeUnit.Infantry:
                    for(int j = 0; j< amount[i]; j++)
                    {
                        troopUnits.Add(Infantryes.ReadUnits[0]);
                        //Infantryes.ReadUnits.Remove(Militias.ReadUnits[0]);
                    }
                    for(int j = 0; j< amount[i]; j++)
                    {
                        //troopUnits.Add(Infantryes.ReadUnits[0]);
                        Infantryes.ReadUnits.Remove(Infantryes.ReadUnits[0]);
                        Infantryes.Amount--;
                    }
                    break;
                case TypeUnit.Cavalry:
                    for(int j = 0; j< amount[i]; j++)
                    {
                        troopUnits.Add(Cavalryes.ReadUnits[0]);
                        //Cavalryes.ReadUnits.Remove(Militias.ReadUnits[0]);
                    }
                    for(int j = 0; j< amount[i]; j++)
                    {
                        //troopUnits.Add(Cavalryes.ReadUnits[0]);
                        Cavalryes.ReadUnits.Remove(Cavalryes.ReadUnits[0]);
                        Cavalryes.Amount--;
                    }
                    break;
            }
        }

        Troop troop = new Troop(troopUnits);
        troop.InitBonus(Attack, AttackOnDefense, Defense);
        Troops.Add(troop);

        return troop;
    }

    public Troop CreateTroopForDefenseCastle()
    {
        List<Unit> units = new List<Unit>();
        
        units.AddRange(Militias.ReadUnits);
        Militias.ReadUnits.Clear();

        units.AddRange(Shooters.ReadUnits);
        Shooters.ReadUnits.Clear();
        
        units.AddRange(Infantryes.ReadUnits);
        Shooters.ReadUnits.Clear();
        
        units.AddRange(Cavalryes.ReadUnits);
        Cavalryes.ReadUnits.Clear();

        Troop troop = new Troop(units);
        
        troop.InitBonus(Attack, AttackOnDefense, Defense);

        return troop;
    }
    
}


public enum TypeUnit
{
    Militia,
    Shooter,
    Infantry,
    Cavalry
}

[Serializable]
public class Troop //класс отряда
{
    public int Speed;
    public List<Unit> Units;
    public float Initiative;
    
    public int bonusAttack, bonusAttackOnDefense, bonusDefense;

    public bool IsAlive => _defense > 0;
    
    private float _attack, _attackOnDefense, _defense;
    


    public Troop(List<Unit> units)
    {
        Units = units;
        
        int minSpeed = 3;
        foreach (var unit in Units)
        {
            if (unit.Speed < minSpeed)
                minSpeed = unit.Speed;
        }
        Speed = minSpeed;
    }

    public void InitBonus(int bonusAttack, int bonusAttackOnDefense, int bonusDefense)
    {
        //инициализация бонус от строений
        this.bonusAttack = bonusAttack;
        this.bonusAttackOnDefense = bonusAttackOnDefense;
        this.bonusDefense = bonusDefense;
    }

    public void InitCharacteristics()
    {
        _attack           = Units.Count * (Units[0].Attack + bonusAttack);
        _attackOnDefense  = Units.Count * (Units[0].Attack + bonusAttackOnDefense) * .75f;
        _defense          = Units.Count * (Units[0].Defense + bonusDefense);
        Initiative       = Units.Count * Speed;
    }

    public void GetDamage(Troop enemyes)
    {
        _defense -= enemyes._attack;
        enemyes._defense -= _attackOnDefense;
    }

    public void GetDamageByShooter(Troop enemyes)
    {
        _defense -= enemyes._attack;
    }
    

}

[Serializable]
public class Unit
{
    public int Attack, Defense, Speed;
    public bool IsReady => _timeToReady == 0;
    public int _timeToReady;

    public Unit(int attack, int defense, int speed, int time)
    {
        Attack = attack;
        Defense = defense;
        Speed = speed;
        _timeToReady = time;
    }

    public void StartTurn()
    {
        _timeToReady--;
    }

    public bool GetReady()
    {
        return _timeToReady == 0;
    }
    
}
[Serializable]
public class Militia
{
    public int PreparationTime = 1;
    public int Cost = 2;
    public int Amount = 0;
    
    public List<Unit> Units;
    public List<Unit> ReadUnits;

    public Militia()
    {
        Units = new List<Unit>();
        ReadUnits = new List<Unit>();
    }
    public void CreateUnit()
    {
        Units.Add(new Unit(2,2,1,1));
    }

    public void StartTurn()
    {
        if (Units.Count > 0)
        {
            for(int i =0;i<Units.Count; i++)
            {
                Units[i].StartTurn();
                ReadyUnits(Units[i]);
            }
            Replace();
        }
    }

    public void ReadyUnits(Unit unit)
    {
        //елси юнит готов, то он переходит в список готовых юнитов
        if (unit.IsReady)
        {       
            Amount++;
            ReadUnits.Add(unit);
        }
    }

    private void Replace()
    {
        if(ReadUnits.Count>0)
            foreach (var readUnit in ReadUnits)
            {
                Units.Remove(readUnit);
            }
    }
    
}
[Serializable]
public class Shooter
{
    public int PreparationTime = 3;
    public int Cost = 8;
    public int Amount = 0;
    
    public List<Unit> Units;
    public List<Unit> ReadUnits;

    public Shooter()
    {
        Units = new List<Unit>();
        ReadUnits = new List<Unit>();
    }
    public void CreateUnit()
    {
        Units.Add(new Unit(3,4,3,3));
    }

    public void StartTurn()
    {
        if (Units.Count > 0)
        {
            for(int i =0;i<Units.Count; i++)
            {
                Units[i].StartTurn();
                ReadyUnits(Units[i]);
            }
            Replace();
        }
    }

    public void ReadyUnits(Unit unit)
    {
        if (unit.IsReady)
        {       
            Amount++;
            ReadUnits.Add(unit);
        }
    }

    private void Replace()
    {
        if(ReadUnits.Count>0)
            foreach (var readUnit in ReadUnits)
            {
                Units.Remove(readUnit);
            }
    }
}
[Serializable]
public class Infantry
{
    public int PreparationTime = 3;
    public int Cost = 20;
    
    public int Amount = 0;
    
    public readonly List<Unit> Units;
    public List<Unit> ReadUnits;

    public Infantry()
    {
        Units = new List<Unit>();
        ReadUnits = new List<Unit>();
    }
    public void CreateUnit()
    {
        Units.Add(new Unit(6,8,1,3));
    }

    public void StartTurn()
    {
        if (Units.Count > 0)
        {
            for(int i =0;i<Units.Count; i++)
            {
                Units[i].StartTurn();
                ReadyUnits(Units[i]);
            }
            Replace();
        }
    }

    public void ReadyUnits(Unit unit)
    {
        if (unit.IsReady)
        {       
            Amount++;
            ReadUnits.Add(unit);
        }
    }

    private void Replace()
    {
        if(ReadUnits.Count>0)
            foreach (var readUnit in ReadUnits)
            {
                Units.Remove(readUnit);
            }
    }
}
[Serializable]
public class Cavalry
{
    public int PreparationTime = 4;
    public int Cost = 48;
    
    public int Amount = 0;
    
    public readonly List<Unit> Units;
    public List<Unit> ReadUnits;

    public Cavalry()
    {
        Units = new List<Unit>();
        ReadUnits = new List<Unit>();
    }
    public void CreateUnit()
    {
        Units.Add(new Unit(20,7,3,4));
    }

    public void StartTurn()
    {
        if (Units.Count > 0)
        {
            for(int i =0;i<Units.Count; i++)
            {
                Units[i].StartTurn();
                ReadyUnits(Units[i]);
            }
            Replace();
        }
    }

    public void ReadyUnits(Unit unit)
    {
        if (unit.IsReady)
        {       
            Amount++;
            ReadUnits.Add(unit);
        }
    }

    private void Replace()
    {
        if(ReadUnits.Count>0)
            foreach (var readUnit in ReadUnits)
            {
                Units.Remove(readUnit);
            }
    }
    
    
}



