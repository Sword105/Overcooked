using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteractable : Interactable
{
    public Transform storedItem = null;
    public AudioClip interactSound;
    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {
            //If there is not a stored item and you are holding something, store the item and remove it from your player in memory
            if (storedItem == null)
            {
                storedItem = heldItem;
                player.GetComponent<PlayerInteraction>().heldItem = null;

                if (interactSound != null)
                {
                    AudioManager.instance.PlaySoundFX(interactSound, transform, 1f);
                }
            }
            else
            {
                //This code is meant to swap the memory location between a held item and a stored item
                //Evidently, it was very buggy

                /*
                Transform temp = heldItem;
                player.GetComponent<PlayerInteraction>().heldItem = storedItem;
                storedItem = temp;
                */
            }
        }
        else
        {
            //If there is a stored item and you are not holding something, remove the item and and place it in your player in memory
            if (storedItem != null)
            {
                player.GetComponent<PlayerInteraction>().heldItem = storedItem;
                storedItem = null;
            }
        }
    }
}
