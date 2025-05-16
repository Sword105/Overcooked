using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageContainer : ContainerInteractable
{
    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {
            //If there is not a stored item and you are holding something, place it down on the table
            if (storedItem == null)
            {
                heldItem.transform.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + heldItem.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
                heldItem.transform.SetParent(transform, true);

                if (heldItem.GetComponent<PlateInteractable>() == null)
                {
                    heldItem.GetComponent<Interactable>().enabled = false;
                }

                base.Interact(player, heldItem);
            }

            else if (storedItem.GetComponent<PlateInteractable>() != null)
            {
                storedItem.GetComponent<PlateInteractable>().Interact(player, heldItem);
            }
        }
        else
        {
            //If there is a stored item, and you aren't holding anything grab it
            if (storedItem != null)
            {
                storedItem.GetComponent<Interactable>().enabled = true;
                storedItem.GetComponent<GrabInteractable>().Interact(player, heldItem);

            }
            base.Interact(player, heldItem);
        }

        //NOTE: base.Interact() calls Interact() is from the ContainerInteractable class
        //This updates the heldItem and storedItem variables in memory
    }
}
