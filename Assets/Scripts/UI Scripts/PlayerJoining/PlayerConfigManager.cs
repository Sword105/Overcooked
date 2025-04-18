using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public PlayerData(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }

    public PlayerInput input;
    public int playerIndex;
    public bool isReady;
    public Mesh playerModel;
}

public class PlayerConfigManager : MonoBehaviour
{
    public List<PlayerData> playerList;
    public string sceneToLoad;

    public Mesh player1Model;
    public Mesh player2Model;

    public static PlayerConfigManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            playerList = new List<PlayerData>();
        }
    }

    public void ReadyPlayer(int index)
    {
        playerList[index].isReady = !playerList[index].isReady;

        if (playerList.All(p => p.isReady == true))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void PlayerJoining(PlayerInput pi)
    {
        pi.transform.SetParent(transform);

        if (!playerList.Any(p => p.playerIndex == pi.playerIndex))
        {
            var newPlayer = new PlayerData(pi);
            if (pi.playerIndex == 0)
            {
                newPlayer.playerModel = player1Model;
            }
            else
            {
                newPlayer.playerModel = player2Model;
            }

            playerList.Add(newPlayer);
        }
    }

}
