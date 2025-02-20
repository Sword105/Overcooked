using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabInteractable : Interactable
{
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
            //If the player is holding nothing
            transform.SetParent(player.transform);
            transform.position = player.transform.position + player.transform.forward;
            transform.rotation = Quaternion.identity;

            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<SphereCollider>().isTrigger = true;
            player.GetComponent<PlayerInteraction>().heldItem = transform;
        }
        else
        {
            //Swaps held and unheld items
            /*
            Vector3 tempPosition = transform.position;
            transform.position = heldItem.position;
            heldItem.position = tempPosition;
            */
        }
    }
}
