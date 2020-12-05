using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArmyUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Text gold;
    [SerializeField] private Text population;
    
    [Space(5)] 
    [SerializeField] private GameObject hireContainer;
    [SerializeField] private GameObject troopContainer;
    
    [Space(5)] 
    [SerializeField] private Text militiaAmount;
    [SerializeField] private Text militiaTimeGS;
    [SerializeField] private Text militiaTroopAvailable;
    [SerializeField] private Text militiaTroopAmount;
    
    [Space(5)] 
    [SerializeField] private Text shooterAmount;
    [SerializeField] private Text shooterTimeGS;
    [SerializeField] private Text shooterTroopAvailable;
    [SerializeField] private Text shooterTroopAmount;

    [Space(5)] 
    [SerializeField] private GameObject hireInfantry;
    [SerializeField] private GameObject troopInfantry;
    [SerializeField] private Text infantryAmount;
    [SerializeField] private Text infantryTimeGS;
    [SerializeField] private Text infantryTroopAvailable;
    [SerializeField] private Text infantryTroopAmount;

    [Space(5)] 
    [SerializeField] private GameObject hireCavalry;
    [SerializeField] private GameObject troopCavalry;
    [SerializeField] private Text cavalryAmount;
    [SerializeField] private Text cavalryTimeGS;
    [SerializeField] private Text cavalryTroopAvailable;
    [SerializeField] private Text cavalryTroopAmount;

    private bool _isHireOpen = false;
    private bool _isTroopOpen = false;

    public void StartPlayerTurn()
    {
        UpdateTimeGS();
        UpdateTroopAvailable();
        UpdateCreateAmount();
    }
    
    public void HireUnits()
    {
        if (_isHireOpen)
            _isHireOpen = false;
        else
            _isHireOpen = true;
        
        hireContainer.SetActive(_isHireOpen);
        hireInfantry.SetActive(player.castle.Barracks.CanBuyInfantry);
        hireCavalry.SetActive(player.castle.Barracks.CanBuyСavalry);
        UpdateResources();
    }

    public void HireMilitia()
    {
        if(player.castle.Resources.Gold < player.castle.Army.Militias.Cost)
            return;
        player.castle.CreateUnitMilitia();
        
        int amount = Int32.Parse(militiaAmount.text) + 1;
        militiaAmount.text = amount.ToString();
        UpdateResources();
    }

    public void HireShooter()
    {
        if(player.castle.Resources.Gold < player.castle.Army.Shooters.Cost)
            return;
        player.castle.CreateUnitShooter();
        
        int amount = Int32.Parse(shooterAmount.text) + 1;
        shooterAmount.text = amount.ToString();

        UpdateResources();
    }

    public void HireInfantry()
    {
        if(player.castle.Resources.Gold < player.castle.Army.Infantryes.Cost)
            return;
        player.castle.CreateUnitInfantry();
        
        int amount = Int32.Parse(infantryAmount.text) + 1;
        infantryAmount.text = amount.ToString();
        UpdateResources();
    }

    public void HireCavalry()
    {
        if(player.castle.Resources.Gold < player.castle.Army.Cavalryes.Cost)
            return;
        player.castle.CreateUnitCavalry();
        
        int amount = Int32.Parse(cavalryAmount.text) + 1;
        cavalryAmount.text = amount.ToString();
        UpdateResources();
    }

    public void TroopUnits()
    {
        if (_isTroopOpen)
            _isTroopOpen = false;
        else
            _isTroopOpen = true;
        
        troopContainer.SetActive(_isTroopOpen);
        troopInfantry.SetActive(player.castle.Barracks.CanBuyInfantry);
        troopCavalry.SetActive(player.castle.Barracks.CanBuyСavalry);
        UpdateResources();
        UpdateTroopAvailable();
    }
    
    public void TroopMilitia()
    {
        int available = Int32.Parse(militiaTroopAvailable.text);
        int amount = Int32.Parse(militiaTroopAmount.text);
        
        if (amount == available)
            return;
        
        amount++;
        militiaTroopAmount.text = amount.ToString();

    }
    
    public void TroopShooter()
    {
        int available = Int32.Parse(shooterTroopAvailable.text);
        int amount = Int32.Parse(shooterTroopAmount.text);
        
        if (amount == available)
            return;
        
        amount++;
        shooterTroopAmount.text = amount.ToString();
    }
    
    public void TroopInfantry()
    {
        int available = Int32.Parse(infantryTroopAvailable.text);
        int amount = Int32.Parse(infantryTroopAmount.text);
        
        if (amount == available)
            return;
        
        amount++;
        infantryTroopAmount.text = amount.ToString();
    }
    
    public void TroopCavalry()
    {
        int available = Int32.Parse(cavalryTroopAvailable.text);
        int amount = Int32.Parse(cavalryTroopAmount.text);
        
        if (amount == available)
            return;
        
        amount++;
        cavalryTroopAmount.text = amount.ToString();
    }

    public void AcceptTroop()
    {
        int amountMilitia   = Int32.Parse(militiaTroopAmount.text);
        int amountShooter   = Int32.Parse(shooterTroopAmount.text);
        int amountInfantry  = Int32.Parse(infantryTroopAmount.text);
        int amountCavalry   = Int32.Parse(cavalryTroopAmount.text);

        List<TypeUnit> typeUnits = new List<TypeUnit>();
        List<int> amounts = new List<int>();

        if (amountMilitia > 0)
        {
            typeUnits.Add(TypeUnit.Militia);
            amounts.Add(amountMilitia);
        }
        if (amountShooter > 0)
        {
            typeUnits.Add(TypeUnit.Shooter);
            amounts.Add(amountShooter);
        }
        if (amountInfantry > 0)
        {
            typeUnits.Add(TypeUnit.Infantry);
            amounts.Add(amountInfantry);
        }
        if (amountCavalry > 0)
        {
            typeUnits.Add(TypeUnit.Cavalry);
            amounts.Add(amountCavalry);
        }

        if (typeUnits.Count == 0 || amounts.Count == 0)
            return;
        
        
        player.castle.CreateTroop(typeUnits, amounts);
        UpdateTroopAmount();
        UpdateTroopAvailable();
    }

    private void UpdateCreateAmount()
    {
        militiaAmount.text = "0";
        shooterAmount.text = "0";
        infantryAmount.text = "0";
        cavalryAmount.text = "0";
        UpdateTroopAvailable();
    }
    
    private void UpdateTroopAmount()
    {
        militiaTroopAmount.text = "0";
        shooterTroopAmount.text = "0";
        infantryTroopAmount.text = "0";
        cavalryTroopAmount.text = "0";
    }
    private void UpdateResources()
    {
        gold.text = player.castle.Resources.Gold.ToString();
        population.text = player.castle.Resources.Population.ToString();
    }

    private void UpdateTimeGS()
    {
        militiaTimeGS.text   = player.castle.Army.Militias.PreparationTime.ToString();
        shooterTimeGS.text   = player.castle.Army.Shooters.PreparationTime.ToString();
        infantryTimeGS.text  = player.castle.Army.Infantryes.PreparationTime.ToString();
        cavalryTimeGS.text   = player.castle.Army.Cavalryes.PreparationTime.ToString();
    }

    private void UpdateTroopAvailable()
    {
        int availableMilitia,availableShooter,availableInfantry,availableCavalry;
        
        availableMilitia = player.castle.Army.ReadyUnits(TypeUnit.Militia);
        availableShooter = player.castle.Army.ReadyUnits(TypeUnit.Shooter);
        availableInfantry = player.castle.Army.ReadyUnits(TypeUnit.Infantry);
        availableCavalry = player.castle.Army.ReadyUnits(TypeUnit.Cavalry);

        militiaTroopAvailable.text = availableMilitia.ToString();
        shooterTroopAvailable.text = availableShooter.ToString();
        infantryTroopAvailable.text = availableInfantry.ToString();
        cavalryTroopAvailable.text = availableCavalry.ToString();
    }
    
    
}
