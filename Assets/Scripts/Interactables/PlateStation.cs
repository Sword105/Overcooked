using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStation : ContainerInteractable
{
    public Stack<PlateInteractable> plateStorage = new Stack<PlateInteractable>();
    public GameObject plateReference;

    void Start()
    {
        //This just sets storedItem to not be empty so that the interaction priority doesn't mess up

        for (int i = 0; i < 6; i++)
        {
            AddPlate();
        }

        storedItem = new GameObject().transform;
    }

    // Delete later on, this is just for testing stacking
    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddPlate();
        }
    }

    public override void Interact(GameObject player, Transform heldItem)
    {
        Debug.Log("plate station detected");
        if (heldItem == null)
        {
            PlateInteractable currentPlate = plateStorage.Pop();

            currentPlate.transform.rotation = Quaternion.identity;
            currentPlate.transform.position = player.transform.position + player.transform.forward * 1.2f;
            currentPlate.transform.SetParent(player.transform, true);

            currentPlate.GetComponent<PlateInteractable>().enabled = true;
            currentPlate.GetComponent<Collider>().isTrigger = true;
            player.GetComponent<PlayerInteraction>().heldItem = currentPlate.transform;
        }
    }

    public void AddPlate()
    {
        //Place the plate on the stack

        Vector3 spawnPosition;
        PlateInteractable prevPlate;

        if (plateStorage.TryPeek(out prevPlate))
        {
            spawnPosition = new Vector3(prevPlate.transform.position.x, prevPlate.transform.GetComponent<MeshRenderer>().bounds.max.y + plateReference.GetComponent<MeshRenderer>().bounds.extents.y - 0.03f, prevPlate.transform.position.z);
        }
        else
        {
            spawnPosition = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + plateReference.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
        }

        GameObject newPlate = Instantiate(plateReference, spawnPosition, Quaternion.identity, transform);
        newPlate.GetComponent<PlateInteractable>().enabled = false;
        newPlate.GetComponent<Rigidbody>().isKinematic = true;
        newPlate.tag = "Untagged";

        plateStorage.Push(newPlate.GetComponent<PlateInteractable>());
    }
}

