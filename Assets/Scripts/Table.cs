using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : ContainerInteractable
{
    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {
            if (storedItem == null)
            {
                heldItem.transform.position = transform.position + new Vector3(0, 1, 0);
                heldItem.transform.SetParent(transform);
            }
            else
            {
                /*
                Vector3 temp = heldItem.position;
                heldItem.position = storedItem.position;
                storedItem.position = temp;
                */
            }
        }
        else
        {
            if (storedItem != null)
            {
                storedItem.GetComponent<GrabInteractable>().Interact(player, heldItem);
                //storedItem = null;
            }
        }

        base.Interact(player, heldItem);
    }
}
