using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    public TextMeshProUGUI titleText;
    public GameObject readyPanel;
    public Button readyButton;

    public void Start()
    {
        MultiplayerEventSystem.current.SetSelectedGameObject(readyButton.gameObject);
    }

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
    }

    public void ReadyPlayer()
    {
        Debug.Log("Ready button pressed");
        PlayerConfigManager.instance.ReadyPlayer(playerIndex);

        if (PlayerConfigManager.instance.playerList[playerIndex].isReady)
        {
            titleText.text = "Ready Up";
        }
        else
        {
            titleText.text = "Cancel";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
