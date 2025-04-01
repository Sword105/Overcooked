using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]

public class PlateInteractable : GrabInteractable
{
    public List<ItemType> foodStored = new List<ItemType>();
    public List<ModelDataStorage> modelResources = new List<ModelDataStorage>();
    private GameObject foodModel;

    //Determines whether a object is grabable based on whether it is being held or not
    void Update()
    {
        if (transform.GetComponentInParent<PlayerInteraction>() != null || transform.GetComponentInParent<PlateStation>() != null)
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
        if (heldItem != null && heldItem.GetComponent<GrabInteractable>().itemType != ItemType.PLATE)
        {
            //If the player is holding nothing, reset the object's rotation, place it in front of the player, and disable its physics
            foodStored.Add(heldItem.GetComponent<GrabInteractable>().itemType);

            if (foodModel == null)
            {
                heldItem.rotation = Quaternion.identity;
                heldItem.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + heldItem.GetComponent<MeshRenderer>().bounds.extents.y - 0.05f, transform.position.z);
                heldItem.SetParent(transform, true);

                heldItem.GetComponent<Rigidbody>().isKinematic = true;
                heldItem.GetComponent<Collider>().isTrigger = true;
                player.GetComponent<PlayerInteraction>().heldItem = null;

                foodModel = heldItem.gameObject;
                foodModel.tag = "Untagged";

                Destroy(foodModel.GetComponent<Interactable>());
            }
            else
            {
                Destroy(heldItem.gameObject);
            }
            

            if (interactSound != null)
            {
                AudioManager.instance.PlaySoundFX(interactSound, transform, 1f);
            }

            UpdateModel();
        }
        else
        {
            base.Interact(player, heldItem);
        }
    }

    //Checks if the plate will be used for a burger
    public void UpdateModel()
    {
        foreach (ModelDataStorage x in modelResources)
        {
            foreach (ModelData y in x.storedModelData)
            {
                if (y.neededItems.Count != foodStored.Count)
                {
                    Debug.Log("skipped");
                    continue;
                }

                bool foundItem = false;
                for (int i = 0; i < y.neededItems.Count; i++)
                {
                    for (int j = 0; j < foodStored.Count; j++)
                    {
                        if (foodStored[j] == y.neededItems[i])
                        {
                            foundItem = true;
                            Debug.Log("CANT FIND IT");
                        }
                    }
                }

                if (foundItem)
                {
                    foodModel.GetComponent<MeshFilter>().mesh = y.model;
                }
            }
        }
    }
}
