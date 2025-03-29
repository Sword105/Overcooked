using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteractable : Interactable
{
    public Transform storedItem = null;
    public AudioClip interactSound;

    //NOTE TO SELF: Remove this and place this line of code in ActionContainers and TimedContainers, but NOT in StorageContainers
    public List<Recipe> recipeList;

    public void Update()
    {
        //Failsafe in case the player grabs the object, but the program forgets to remove the storedItem from memory
        //Note: I do not know why this issue happens sometimes for some people but not for others
        if (storedItem != null && transform.GetComponentInChildren<GrabInteractable>() == null)
        {
            storedItem = null;
        }
    }

    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
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

    //This may be useful, finds a recipe given two Food objects
    public Recipe FindRecipe(ItemType item)
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
