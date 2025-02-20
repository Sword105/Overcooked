using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject player;

    public float range = 0.8f;
    public float distanceForward = 1.3f;

    public Transform heldItem = null;

    void Update()
    {
        //Array of objects within the radius in front of the player
        Collider[] nearbyObjects = Physics.OverlapSphere(player.transform.position + (player.transform.forward * distanceForward), range);

        Collider nearest = null;
        float smallestDistance = 10000;
        foreach (Collider other in nearbyObjects)
        {
            //Finds the closest interactable object
            float currentDistance = Vector3.Distance(player.transform.position, other.transform.position);

            if (currentDistance < smallestDistance && other.transform.GetComponent<Interactable>() != null && !other.transform.Equals(heldItem))
            {
                //This prioritizes grabbing over interactions with appliances when the player is empty-handed
                if (heldItem == null && other.GetComponent<GrabInteractable>() == null && other.GetComponent<ContainerInteractable>().storedItem == null)
                {
                    continue;
                }
                smallestDistance = currentDistance;
                nearest = other;
            }
        }

        //Interacts with the closest interactable object
        if (Input.GetKeyDown(KeyCode.Space) && nearest != null && nearest.GetComponent<Interactable>() != null)
        {
            Debug.Log("Interactable detected, trying interaction");
            nearest.GetComponent<Interactable>().Interact(player, heldItem);
        }

        //Drops held object
        if (Input.GetKeyDown(KeyCode.Q) && heldItem != null)
        {
            heldItem.transform.SetParent(null);

            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.GetComponent<SphereCollider>().isTrigger = false;

            heldItem = null;   
        }

        //Bug fix lol
        if (heldItem != null && transform.GetComponentInChildren<GrabInteractable>() == null)
        {
            Transform temp = heldItem;
            heldItem = null;
            temp.GetComponentInChildren<GrabInteractable>().Interact(player, null);
        }
    }
}

