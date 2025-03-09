using System.Collections;
using System.Collections.Generic;
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
                heldItem.transform.position = transform.position + new Vector3(0, 1, 0);
                heldItem.transform.SetParent(transform, true);
            }
            else
            {
                //This code is meant to swap the positions between a held item and a store item
                //Evidently, it was very buggy

                /*
                Vector3 temp = heldItem.position;
                heldItem.position = storedItem.position;
                storedItem.position = temp;
                */
            }
        }
        else
        {
            //If there is a stored item, and you aren't holding anything grab it
            if (storedItem != null)
            {
                storedItem.GetComponent<GrabInteractable>().Interact(player, heldItem);
            }
        }

        //Interact method from the ContainerInteractable class
        //This updates the heldItem and storedItem variables in memory
        base.Interact(player, heldItem);
    }
}
