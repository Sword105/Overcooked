using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]

public class PlateInteractable : GrabInteractable
{
    public List<ItemType> foodStored = new List<ItemType>();
    [SerializeField] private GameObject foodModel;

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
        if (heldItem != null)
        {
            //If the player is holding nothing, reset the object's rotation, place it in front of the player, and disable its physics
            foodStored.Add(heldItem.GetComponent<GrabInteractable>().itemType);

            if (foodModel == null)
            {
                heldItem.rotation = Quaternion.identity;
                heldItem.position = transform.position + transform.up * 1.2f;
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
        }
        else
        {
            base.Interact(player, heldItem);
        }
    }

    //Checks if the plate will be used for a burger
    public void BurgerCheck(GameObject foodModel)
    {
        if (foodStored[0] == ItemType.BURGER_BUNS)
        {
            // Change the model depending on following if-statements
        }
    }
}
