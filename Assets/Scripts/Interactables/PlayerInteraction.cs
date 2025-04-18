using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/*
public static class PlayerEvent
{
    public static event Action<Collider> sendPlayerData;

    public static void callEvent(Collider nearestInteractable)
    {
        sendPlayerData?.Invoke(nearestInteractable);
    }
}
*/

public class PlayerInteraction : MonoBehaviour
{
    //FYI, "NonSerialized" just hides the variable from the inspector

    [NonSerialized] public GameObject player;
    public Transform heldItem = null;
    private bool isInteracting = false;

    public float interactionRange = 0.8f;
    public float interactionForwardOffset = 1.3f;

    /*[NonSerialized]*/ public Collider nearestInteractable = null;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInteracting = true;
        }
        //Debug.Log(isInteracting);
    }

    private void Start()
    {
        player = this.gameObject;
    }

    void Update()
    {
        //Gets an array of objects within a radius in front of the player and finds the closest one
        Vector3 interactionLocation = player.transform.position + (player.transform.forward * interactionForwardOffset);
        Collider[] nearbyObjects = Physics.OverlapCapsule(interactionLocation + new Vector3(0, 1, 0), interactionLocation + new Vector3(0, -1, 0), interactionRange);
        nearestInteractable = FindClosestInteractable(nearbyObjects);

        //Interacts with the closest interactable object
        if (isInteracting && nearestInteractable != null && nearestInteractable.GetComponent<Interactable>() != null)
        {
            Debug.Log("Interactable detected as " + nearestInteractable.name + ", trying interaction");
            nearestInteractable.GetComponent<Interactable>().Interact(player, heldItem);
        }
        //Drops held object and enables its physics
        else if (isInteracting && heldItem != null)
        {
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.GetComponent<Collider>().isTrigger = false;

            heldItem.transform.SetParent(null, true);
            heldItem = null;

            Debug.Log("Dropping held item");
        }

        //Failsafe in case two players interact with an object at the same time
        if (heldItem != null && transform.GetComponentInChildren<GrabInteractable>() == null)
        {
            Transform temp = heldItem;
            heldItem = null;
            temp.GetComponentInChildren<GrabInteractable>().Interact(player, null);
        }

        isInteracting = false;
    }

    //Finds the closest interactable from a range of objects (meant to be used alongside Physics.OverlapSphere()) 
    Collider FindClosestInteractable(Collider[] objects)
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
                if (heldItem == null && (other.GetComponent<GrabInteractable>() == null || !other.GetComponent<GrabInteractable>().isActiveAndEnabled)
                    && (other.GetComponent<ContainerInteractable>() == null
                    || other.GetComponent<ContainerInteractable>().storedItem == null
                    && other.CompareTag("Grabbable")))
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

