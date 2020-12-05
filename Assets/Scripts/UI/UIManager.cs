using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Text gold;
    [SerializeField] private Text population;
    
    [Space(5)]
    [SerializeField] private Text townHallLevelText;
    [SerializeField] private Text townHallCostText;
    
    [Space(5)]
    [SerializeField] private Text residentBuildingsLevelText;
    [SerializeField] private Text residentBuildingsCostText;
    
    [Space(5)]
    [SerializeField] private Text churchLevelText;
    [SerializeField] private Text churchCostText;
    
    [Space(5)]
    [SerializeField] private Text wallsLevelText;
    [SerializeField] private Text wallsCostText;
    
    [Space(5)]
    [SerializeField] private Text barracksLevelText;
    [SerializeField] private Text barracksCostText;

   
    
    

    private string _template = "LEVEL: ";

    public void StartPlayerTurn()
    {
        UpdateResources();
        UpdateCost();
    }

    public void UpgradeTownHall()
    {
        player.castle.UpgradeTownHall();
        townHallLevelText.text = _template + player.castle.TownHall.Level.ToString();
        UpdateResources();
        UpdateCost();
    }
    
    public void UpgradeResidentBuildings()
    {
        player.castle.UpgradeResidentBuildings();
        residentBuildingsLevelText.text = _template + player.castle.ResidentBildings.Level.ToString();
        UpdateResources();
        UpdateCost();
    }
    
    public void UpgradeChurch()
    {
        player.castle.UpgradeChurch();
        churchLevelText.text = _template + player.castle.Church.Level.ToString();
        UpdateResources();
        UpdateCost();
    }
    
    public void UpgradeWalls()
    {
        player.castle.UpgradeWalls();
        wallsLevelText.text = _template + player.castle.Walls.Level.ToString();
        UpdateResources();
        UpdateCost();
    }
    
    public void UpgradeBarracks()
    {
        player.castle.UpgradeBarracks();
        barracksLevelText.text = _template + player.castle.Barracks.Level.ToString();
        UpdateResources();
        UpdateCost();
    }

    
    
    private void UpdateResources()
    {
        gold.text = player.castle.Resources.Gold.ToString();
        population.text = player.castle.Resources.Population.ToString();
    }

    private void UpdateCost()
    {
        townHallCostText.text = player.castle.TownHall.CostUpgrade.ToString();
        residentBuildingsCostText.text = player.castle.ResidentBildings.CostUpgrade.ToString();
        churchCostText.text = player.castle.Church.CostUpgrade.ToString();
        wallsCostText.text = player.castle.Walls.CostUpgrade.ToString();
        barracksCostText.text = player.castle.Barracks.CostUpgrade.ToString();
    }
    
    
}
