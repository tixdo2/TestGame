using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class СombatUnit : MonoBehaviour
{
    public Troop Units;

    private Castle _castle;
    
    public bool IsPlayerUnits;
    public bool IsSelected = false;
    public bool CanMove => _speed > 0;
    
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material materialEnter;
    [SerializeField] private Material _defaulMaterial;

    private List<Plane> _planesSelected = new List<Plane>();
    
    
    private Troop _militias;
    private Troop _shooters;
    private Troop _infantryes;
    private Troop _cavalryes;
    
    // упорядоченный список подотрядов по их инициативе
    private List<Troop> Troops = new List<Troop>();

    //количество клеток, которые может пройти отряд на этом ходу
    [SerializeField]private int _speed ;
    

    public void StartTurn()
    {
        _speed = Units.Speed;
    }
    public void OnMouseEnter()
    {
        if (IsPlayerUnits)
            _meshRenderer.material = materialEnter;
    }

    public void OnMouseExit()
    {
        if(IsPlayerUnits)
            _meshRenderer.material = _defaulMaterial;
    }

    

    public void Init(Castle castle)
    {
        _castle = castle;
        _speed = Units.Speed;
        List<Unit> militias = new List<Unit>();
        List<Unit> shooters = new List<Unit>();
        List<Unit> infantryes = new List<Unit>();
        List<Unit> cavalryes = new List<Unit>();
        
        foreach(var unit in Units.Units)
        {
            switch (unit.Attack)
            {
                case 2: militias.Add(unit); break;
                case 4: shooters.Add(unit); break;
                case 8: infantryes.Add(unit); break;
                case 20: cavalryes.Add(unit); break;
            }
        }
        if(militias.Count > 0)
        {
            _militias = new Troop(militias);
            _militias.InitBonus(Units.bonusAttack, Units.bonusAttackOnDefense, Units.bonusDefense);
            _militias.InitCharacteristics();
            Troops.Add(_militias);
        }
        
        if(shooters.Count > 0)
        {
            _shooters = new Troop(shooters);
            _shooters.InitBonus(Units.bonusAttack, Units.bonusAttackOnDefense, Units.bonusDefense);
            _shooters.InitCharacteristics();
            Troops.Add(_shooters);
        }

        if(infantryes.Count > 0)
        {
            _infantryes = new Troop(infantryes);
            _infantryes.InitBonus(Units.bonusAttack, Units.bonusAttackOnDefense, Units.bonusDefense);
            _infantryes.InitCharacteristics();
            Troops.Add(_infantryes);
        }

        if(cavalryes.Count > 0)
        {
            _cavalryes = new Troop(cavalryes);
            _cavalryes.InitBonus(Units.bonusAttack, Units.bonusAttackOnDefense, Units.bonusDefense);
            _cavalryes.InitCharacteristics();
            Troops.Add(_cavalryes);
        }
        
        SortTroop();
    }
    
    public void Battle(СombatUnit combatUnitEnemy)
    {
        //Проверка на объект атаки
        while (Troops.Count > 0 || combatUnitEnemy.Troops.Count > 0) //пока жив один из отрядов
            {
                foreach (var troop in Troops) // атака каждого подотряда игрока 
                {
                    if (troop.Units[0].Attack != 4) //проверка, если атакует не лучник
                        combatUnitEnemy.Troops.Last().GetDamage(troop);
                    else
                        combatUnitEnemy.Troops.Last().GetDamageByShooter(troop);

                    if (!combatUnitEnemy.Troops.Last().IsAlive) // если подотряд не жив, то удаляется
                        combatUnitEnemy.Troops.Remove(combatUnitEnemy.Troops.Last());
                }

                foreach (var enemyTroop in combatUnitEnemy.Troops) // атака каждого подотряда противника 
                {
                    if (enemyTroop.Units[0].Attack != 4)
                        Troops.Last().GetDamage(enemyTroop);
                    else
                        Troops.Last().GetDamageByShooter(enemyTroop);

                    if (!Troops.Last().IsAlive)
                        Troops.Remove(Troops.Last());
                }
            }
            // проверка на живы ли отряды, если уничтожены, то удаляются со сцены
            combatUnitEnemy.TryDestroyCombatUnit(); 
            TryDestroyCombatUnit();
    }
    

    public void TryDestroyCombatUnit()
    {
        if (Troops.Count == 0)
        {
            _castle.DieCombatUnit(this);
            Destroy(gameObject);
        }
    }
    
    //сортировка подотрядов по инициативе
    private void SortTroop()
    {
        for(int i =0; i< Troops.Count; i++)
            for (int j = 0; j < Troops.Count - 1; j++)
            {
                if (Troops[j].Initiative < Troops[j + 1].Initiative)
                {
                    Troop temp = Troops[j];
                    Troops[j] = Troops[j + 1];
                    Troops[j + 1] = temp;
                }
            }
    }


    private void Update()
    {
        
    }

    //выбирает и окрашивает платформы, куда игрок может передвинуть своё войско
    public void Selected()
    {
        if (!IsSelected)
            return;
        
        Vector3 rayStart = transform.position;
        
        Vector3 rayEndBack = Vector3.down;
        Vector3 rayEndLeft = Vector3.down;
        Vector3 rayEndRight = Vector3.down;
        Vector3 rayEndForward = Vector3.down;
        
        rayStart.y = 2f;
        List<Vector3> raysEndPoints = new List<Vector3>();
        
        rayEndBack.z += 2.5f * Units.Speed;
        rayEndLeft.x += 2.5f * Units.Speed;
        rayEndRight.x -= 2.5f * Units.Speed;
        rayEndForward.z -= 2.5f * Units.Speed;

        for (int i = 0; i < 4; i++)
        {   
            raysEndPoints.Add(new Vector3());
            for (int j = 1; j <= _speed; j++)
            {
                raysEndPoints[i] = Vector3.down;

                var raysEndPoint = raysEndPoints[i];
                
                switch (i)
                {
                    case 0:
                        raysEndPoint.z += 2.5f * j;
                        raysEndPoints[0] = raysEndPoint;
                        break;
                    case 1:
                        raysEndPoint.z -= 2.5f * j;
                        raysEndPoints[1] = raysEndPoint;
                        break;
                    case 2:
                        raysEndPoint.x -= 2.5f * j;
                        raysEndPoints[2] = raysEndPoint;
                        break;
                    case 3:
                        raysEndPoint.x += 2.5f * j;
                        raysEndPoints[3] = raysEndPoint;
                        break;
                }
                Debug.DrawRay(rayStart,  raysEndPoints[i]*500, Color.blue);

                if (Physics.Raycast(new Ray(rayStart, raysEndPoints[i]), out RaycastHit rayBackHit))
                {
                    if(rayBackHit.collider.TryGetComponent<Plane>(out Plane plane))
                    {
                        plane.CanMove();
                        plane.Cost = j;
                        _planesSelected.Add(plane);
                    }
                }
                Debug.Log(_planesSelected.Count);
            }
        }
    }

    //передвижение отряда
    public void Move(Plane plane)
    {
        Debug.Log(111111);
        if (!CanMove)
            return;
        IsSelected = false;
        _speed-=plane.Cost;
        plane.canMove = false;
        transform.position = plane.transform.position;
        PlaneDeselecte();
    }

    public void PlaneDeselecte()
    {
        foreach (var plane in _planesSelected)
        {
            plane.Deselecte();
        }
        _planesSelected.Clear();
    }
}
