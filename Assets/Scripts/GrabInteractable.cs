using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabInteractable : Interactable
{
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
        if (heldItem == null && transform.tag == "Grabbable")
        {
            //If the player is holding nothing, reset the object's rotation, place it in front of the player, and disable its physics
            transform.SetParent(player.transform);
            transform.position = player.transform.position + player.transform.forward;
            transform.rotation = Quaternion.identity;

            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<SphereCollider>().isTrigger = true;
            player.GetComponent<PlayerInteraction>().heldItem = transform;
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
