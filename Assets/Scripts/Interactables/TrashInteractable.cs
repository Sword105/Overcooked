using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteractable : ContainerInteractable
{
    void Start()
    {
        this.GetComponent<OutlineThing>().OutlineWidth = 5;
        this.GetComponent<OutlineThing>().enabled = false;
    }

    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {
            if (heldItem.GetComponent<PlateInteractable>() != null)
            {
                heldItem.GetComponent<PlateInteractable>().EmptyPlate();
            }
            else
            {
                Destroy(heldItem.gameObject);
                player.GetComponent<PlayerInteraction>().heldItem = null;
            }
        }
    }
}
