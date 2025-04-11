using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject playerPrefab;

    private void Start()
    {
        var playerConfigs = PlayerConfigManager.instance.playerList.ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject player = Instantiate(playerPrefab, playerSpawns[i].position + new Vector3(0,5,0), playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().StartPlayer(playerConfigs[i]);
        }
    }
}
