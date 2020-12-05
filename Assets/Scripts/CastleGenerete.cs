using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGenerete : MonoBehaviour
{
    [SerializeField] private GameObject castleEnemyPrefab;
    [SerializeField] private GameObject castlePlayerPrefab;
    [SerializeField] private CameraMover camera;

    [SerializeField] private Turn turn;
    
    public EnemyCastle InitEnemyCastles(int size, int castleID)
    {
        int posX = Random.Range(0, size)*5;
        int posZ = Random.Range(0, size)*5;
        var castle = Instantiate(castleEnemyPrefab, new Vector3(posX, 0.5f, -posZ), Quaternion.identity).GetComponent<EnemyCastle>();
        var ai = castle.GetComponent<EnemyAI>();
        ai.castle = castle;
        ai.turn = turn;
        castle.CastleID = castleID+1;
        turn.LastID = castleID + 1;
        ai.InitEvents();
        return castle;
    }

    public PlayerCastle InitPlayerCastle(int size, Player player)
    {
        int posX = Random.Range(0, size)*5;
        int posZ = Random.Range(0, size)*5;
        var castle = Instantiate(castlePlayerPrefab, new Vector3(posX, 0.5f, -posZ), Quaternion.identity).GetComponent<PlayerCastle>();
        player.castle = castle;
        //player.turn = turn;
        castle.CastleID = 0;
        camera.InitPosition(castle.transform.position);
        player.InitEvents();
        player.StartGame();
        return castle;
    }
    
}



