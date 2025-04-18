using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainerInteractable : ContainerInteractable
{
    //These variables track the interactions required and left in the container.    
    public int requiredInteractions = 8;
    public int remainingInteractions;
    private bool foodReady = false;

    
    
    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {

            //If there is not a stored item and you are holding something, place it down on the table
            if (storedItem == null && findInputItemInRecipeList(heldItem.GetComponent<GrabInteractable>().itemType) != -1)
            {
                heldItem.transform.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + heldItem.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
                heldItem.transform.SetParent(transform, true);
                base.Interact(player, heldItem);
                
                //resets the count of the interactions when a new item is placed in the ActionContainer
                remainingInteractions = requiredInteractions;
                
            }
            else
            {
                Debug.Log("Not in recipeList");
            }
        }
        //Reduces the number of remainingInteractions, and if it is = 0, cooks the item
        else if(foodReady == false)
        {
            remainingInteractions -= 1;
            Debug.Log("Remaining Interactions: " + remainingInteractions);

            //Cook the item
            if(remainingInteractions <= 0){
                cook();
                
                remainingInteractions = requiredInteractions;
                
            }
        }
        else
        {
            //If there is a stored item, foodReady == true, and you aren't holding anything grab it
            if (storedItem != null && foodReady == true)
            {
                base.Interact(player, heldItem);
                foodReady = false;
                
            }
                
        
        }

        //NOTE: base.Interact() calls Interact() is from the ContainerInteractable class
        //This updates the heldItem and storedItem variables in memory
    }



    // When remainingInteractions <=0, this function is called, and we get the new item
    void cook(){
        
        int inputRecipeList = findInputItemInRecipeList(storedItem.GetComponent<GrabInteractable>().itemType);
       
        if(storedItem != null && inputRecipeList != -1){

            //Instantiating the cookedMeal, and destroying the ingredient's GameObject
            GameObject cookedMeal = Instantiate(recipeList[inputRecipeList].outputGameObject, storedItem.position, storedItem.rotation);

            GameObject itemToEliminate = storedItem.gameObject;
            storedItem = cookedMeal.transform;
            //storedItem.tag = "Untagged";
            Destroy(itemToEliminate);
            


            //Disabling the physics and object rotation of the cookedMeal
            storedItem.transform.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + storedItem.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
            storedItem.transform.SetParent(transform, true);
            storedItem.GetComponent<Rigidbody>().isKinematic = true;
            storedItem.GetComponent<Collider>().isTrigger = true;
            
            foodReady = true;
            Debug.Log("The food is ready!");
        }

    }
    
    
    //Finds the input of a recipe in the recipe list based on the inputFoodItem
    int findInputItemInRecipeList(ItemType i){

        int count = 0;
        foreach (Recipe recipe in recipeList)
        {
            if (recipe.inputFoodItem == i)
            {
                return count;
            }

            count++;
        }
        return -1;
    }

    //This is used in GrabInteractable, so we can get the this info to set the tag to "Grabbable" or "Untagged"
    public bool getFoodReady()
    {
        return foodReady;
    }
}
