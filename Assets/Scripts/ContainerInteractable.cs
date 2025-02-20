using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteractable : Interactable
{
    public Transform storedItem = null;
    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {
            if (storedItem == null)
            {
                storedItem = heldItem;
                player.GetComponent<PlayerInteraction>().heldItem = null;
            }
            else
            {
                /*
                Transform temp = heldItem;
                player.GetComponent<PlayerInteraction>().heldItem = storedItem;
                storedItem = temp;
                */
            }
        }
        else
        {
            if (storedItem != null)
            {
                player.GetComponent<PlayerInteraction>().heldItem = storedItem;
                storedItem = null;
            }
        }
    }
}
