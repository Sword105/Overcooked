using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedContainerInteractable : ContainerInteractable
{

    //public List<Recipe> recipeList;


    public override void Interact(GameObject player, Transform heldItem)
    {
        //Check if the player is holding something, and that item is in the recipeList
        if (heldItem != null /* && findInputItemInRecipeList(heldItem.GetComponent<GrabInteractable>().itemType) != -1 */)
        {
            //If there is not a stored item and you are holding something, store the item and remove it from your player in memory

            if (storedItem == null)
            {

                
                storedItem = heldItem;
                player.GetComponent<PlayerInteraction>().heldItem = null;

                if (interactSound != null)
                {
                    AudioManager.instance.PlaySoundFX(interactSound, transform, 1f);
                }
                

                cook(storedItem, findInputItemInRecipeList(heldItem.GetComponent<GrabInteractable>().itemType));
                
            }
        }
        else
        {
            //If there is a stored item and you are not holding something, remove the item and and place it in your player in memory
            
            if (storedItem != null)
            {
                player.GetComponent<PlayerInteraction>().heldItem = storedItem;
                storedItem = null;
            }
        }
    }

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

    void cook(Transform ingredient, int inputInRecipeList){
        
       
        StartMyTimer();

        Debug.Log("Cooked");
        
    }



    public void StartMyTimer()
    {
        StartCoroutine(MyTimerCoroutine(5f)); // Start a 5-second timer

        Debug.Log("Testing");
    }

    private IEnumerator MyTimerCoroutine(float duration)
    {
        Debug.Log("Timer started");
        yield return new WaitForSeconds(duration);
        Debug.Log("Timer finished!");
        // Do something here, like activate a GameObject:
        // myObject.SetActive(true);
    }



}
