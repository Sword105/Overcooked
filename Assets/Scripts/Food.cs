using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food class to store the name of a food
public class Food : GrabInteractable
{
    // The name of the Food
    public string name;
    // Some meals may have a Recipe
    public Recipe recipe;
    //The food item itself
    public GameObject foodObject;


    
    
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
        else if(transform.tag == "Grabbable")
        {

            //If two objects are part of a recipe, and that recipe is in the RecipesList (In the player object), creates the object of the recipe and destroys the other two.
            Collider[] nearbyObjects = Physics.OverlapSphere(player.transform.position + 
            (player.transform.forward * player.GetComponent<PlayerInteraction>().interactionForwardOffset), player.GetComponent<PlayerInteraction>().interactionRange);
         
            Collider nearestInteractable = player.GetComponent<PlayerInteraction>().FindClosestInteractable(nearbyObjects);
            
            if(player.GetComponent<RecipesList>().FindRecipe(this, player.GetComponent<PlayerInteraction>().heldItem.GetComponent<Food>()) != null){
            Debug.Log("Cooking available");
            GameObject result = Instantiate(player.GetComponent<RecipesList>().FindRecipe(this, player.GetComponent<PlayerInteraction>().heldItem.GetComponent<Food>()).result.foodObject,
             transform.position, transform.rotation);

             Destroy(this.foodObject);
             Destroy(player.GetComponent<PlayerInteraction>().heldItem.GetComponent<Food>().foodObject);

             result.transform.SetParent(player.transform);
             result.transform.position = player.transform.position + player.transform.forward;
             result.transform.rotation = Quaternion.identity;
             
             result.transform.GetComponent<Rigidbody>().isKinematic = true;
             result.transform.GetComponent<SphereCollider>().isTrigger = true;
             player.GetComponent<PlayerInteraction>().heldItem = result.transform;


             

            }
            else{
                Debug.Log("Failed to cook");
                Debug.Log("Object 1: " + this.name);
                Debug.Log("Object 2: " + nearestInteractable.GetComponent<Food>().name);
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

