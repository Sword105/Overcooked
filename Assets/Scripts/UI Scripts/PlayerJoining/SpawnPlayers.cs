using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    [NonSerialized] public List<PlayerInteraction> players = new List<PlayerInteraction>();

    private void Start()
    {
        var playerConfigs = PlayerConfigManager.instance.playerList.ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject player;
            //player = Instantiate(player1Prefab, playerSpawns[i].position + new Vector3(0, 5, 0), playerSpawns[i].rotation, gameObject.transform);

            
            if (i == 0)
            {
                player = Instantiate(player1Prefab, playerSpawns[i].position + new Vector3(0, 0.95f, 0), playerSpawns[i].rotation, null);
            }
            else if (i == 1)
            {
                player = Instantiate(player2Prefab, playerSpawns[i].position + new Vector3(0, 0.95f, 0), playerSpawns[i].rotation, null);
            }
            else
            {
                player = null;
            }
            

            player.GetComponent<PlayerInputHandler>().StartPlayer(playerConfigs[i]);
            players.Add(player.GetComponent<PlayerInteraction>());
        }

        PlayerInfo.setSpawnPlayers(this);
    }
}

public static class PlayerInfo
{
    private static SpawnPlayers spawnPlayers;

    public static void setSpawnPlayers(SpawnPlayers sp)
    {
        spawnPlayers = sp;
    }

    public static List<PlayerInteraction> listOfPlayers()
    {
        return spawnPlayers.players;
    }
}
