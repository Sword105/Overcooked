using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]

public class GrabInteractable : Interactable
{
    //public AudioSource audioSource;
    public AudioClip interactSound;
    public ItemType itemType; 

    //Determines whether a object is grabable based on whether it is being held or not
    void Update()
    {
        if (transform.GetComponentInParent<PlayerInteraction>() != null)
        {
            transform.tag = "Untagged";
        }
        else
        {
            transform.tag = "Grabbable";
        }
    }

    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem == null && transform.CompareTag("Grabbable"))
        {
            //If the player is holding nothing, reset the object's rotation, place it in front of the player, and disable its physics
            transform.rotation = Quaternion.identity;
            transform.position = player.transform.position + player.transform.forward * 1.2f;
            transform.SetParent(player.transform, true);

            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Collider>().isTrigger = true;
            player.GetComponent<PlayerInteraction>().heldItem = transform;

            if (interactSound != null)
            {
                AudioManager.instance.PlaySoundFX(interactSound, transform, 1f);
            }
        }
        else
        {
            //This code is meant to swap the location between a held item and an item on the floor
            //Evidently, it was very buggy

            /*
            Vector3 tempPosition = transform.position;
            transform.position = heldItem.position;
            heldItem.position = tempPosition;
            */
        }
    }
}
