using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInteractable : ContainerInteractable
{
    public GrabInteractable itemToSpawn;

    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem == null)
        {
            GrabInteractable newItem = Instantiate(itemToSpawn, transform);
            newItem.Interact(player, heldItem);
        }
    }
}
