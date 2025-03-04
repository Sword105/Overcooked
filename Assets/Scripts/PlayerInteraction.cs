using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject player;
    public Transform heldItem = null;

    public float interactionRange = 0.8f;
    public float interactionForwardOffset = 1.3f;

    void Update()
    {
        //Gets an array of objects within a radius in front of the player and finds the closest one
        Collider[] nearbyObjects = Physics.OverlapSphere(player.transform.position + (player.transform.forward * interactionForwardOffset), interactionRange);
        Collider nearestInteractable = FindClosestInteractable(nearbyObjects);

        //Interacts with the closest interactable object
        if (Input.GetKeyDown(KeyCode.Space) && nearestInteractable != null && nearestInteractable.GetComponent<Interactable>() != null)
        {
            Debug.Log("Interactable detected, trying interaction");
            nearestInteractable.GetComponent<Interactable>().Interact(player, heldItem);
        }

        //Drops held object and enables its physics
        if (Input.GetKeyDown(KeyCode.Q) && heldItem != null)
        {
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.GetComponent<SphereCollider>().isTrigger = false;

            heldItem.transform.SetParent(null);
            heldItem = null;   
        }

        //Failsafe in case two players interact with an object at the same time
        if (heldItem != null && transform.GetComponentInChildren<GrabInteractable>() == null)
        {
            Transform temp = heldItem;
            heldItem = null;
            temp.GetComponentInChildren<GrabInteractable>().Interact(player, null);
        }
    }

    //Finds the closest interactable from a range of objects (meant to be used alongside Physics.OverlapSphere()) 
    public Collider FindClosestInteractable(Collider[] objects)
    {
        Collider nearest = null;
        float smallestDistance = 10000;
        foreach (Collider other in objects)
        {
            //Finds the closest interactable object that is not the held item
            float currentDistance = Vector3.Distance(player.transform.position, other.transform.position);

            if (currentDistance < smallestDistance && other.transform.GetComponent<Interactable>() != null && !other.transform.Equals(heldItem))
            {
                //Ignore if the interactable is NOT a grabable object or if it is an empty container object
                if (heldItem == null && other.GetComponent<GrabInteractable>() == null && other.GetComponent<ContainerInteractable>().storedItem == null)
                {
                    continue;
                }
                smallestDistance = currentDistance;
                nearest = other;
            }
        }

        return nearest;
    }
}

