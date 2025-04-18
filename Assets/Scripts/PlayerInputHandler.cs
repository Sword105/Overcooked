using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField]
    private MovementScript3 movementScript;
    [SerializeField]
    private PlayerInteraction interactionScript;

    private PlayerControls controls;


    // Start is called before the first frame update
    void Awake()
    {
        //movementScript.GetComponent<MovementScript3>();
        //interactionScript.GetComponent<PlayerInteraction>();

        controls = new PlayerControls();
    }

    public void StartPlayer(PlayerData data)
    {
        playerData = data;
        playerData.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.player.Movement.name)
        {
            movementScript.OnMove(obj);
        }

        if (obj.action.name == controls.player.Dash.name)
        {
            //movementScript.OnDash(obj);
        }

        if (obj.action.name == controls.player.Interact.name)
        {
            interactionScript.OnInteract(obj);
        }
    }
}
