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
    public List<Recipe> recipeList;
    public GameObject grabInteractable;

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

            if (interactSound != null)
            {
                AudioManager.instance.PlaySoundFX(interactSound, transform, 1f);
            }
        }
        else if (transform.tag == "Grabbable"){

            Collider[] nearbyObjects = Physics.OverlapSphere(player.transform.position + 
            (player.transform.forward * player.GetComponent<PlayerInteraction>().interactionForwardOffset), player.GetComponent<PlayerInteraction>().interactionRange);

             Collider nearestInteractable = player.GetComponent<PlayerInteraction>().FindClosestInteractable(nearbyObjects);

             //Checking if we can combine two Food objects, and doing so
            
            if (FindRecipe(nearestInteractable.GetComponent<GrabInteractable>().itemType)!= null){
                Debug.Log("Cooking available");
                //GameObject result = Instantiate(FindRecipe(nearestInteractable.GetComponent<GrabInteractable>().itemType).outputFoodItem)
            }

        }
        {
            //This code is meant to swap the location between a held item and an item on the floor
            //Evidently, it was very buggy

            /*
            Vector3 tempPosition = transform.position;
            transform.position = heldItem.position;
            heldItem.position = tempPosition;
            */
        }

        Recipe FindRecipe(ItemType item)
    {
        foreach (Recipe recipe in recipeList)
        {
            if (recipe.inputFoodItem == item)
            {
                return recipe;
            }
        }
        return null;
    }
    }
}
