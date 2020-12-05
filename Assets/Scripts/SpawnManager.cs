using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int CountBots;
    public int Turn;
    public SpawnManager Game;

    [SerializeField] private PlaneGrid planeGrid;
    [SerializeField] private CastleGenerete castleGenerete;
    [SerializeField] private int size;
    [SerializeField] private Player player;
    
    private List<EnemyCastle> _enemyCastles;
    private PlayerCastle _playerCastle;

    private void Awake()
    {
        Game = this;
    }


    private void Start()
    {
        size = Random.Range(15, 50);
        CountBots = Random.Range(3, 6);
        _enemyCastles = new List<EnemyCastle>(CountBots);
        planeGrid.InitMap(size);
        for(int i = 0; i < CountBots; i++)
            _enemyCastles.Add(castleGenerete.InitEnemyCastles(size, i));
        _playerCastle = castleGenerete.InitPlayerCastle(size, player);
    }
}


