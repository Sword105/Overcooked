using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedContainerInteractable : ContainerInteractable
{

    //public List<Recipe> recipeList;
    public float timerDuration = 5f; // total time in seconds
    public float burnTime = -8f;
    public float timer;
    private bool isTiming = false;
    private bool foodReady = false;

    public GameObject burnedObject;


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

                StartTimer();
            }
            else //storedItem = null
            {
                Debug.Log("Not in recipeList");
            }
        }
        else if(foodReady == false)
        {
            Debug.Log("Not possible to grab an item while cooking");
        }
        else
        {
            //If there is a stored item, and you aren't holding anything grab it
            if (storedItem != null && foodReady == true)
            {
                base.Interact(player, heldItem);
                foodReady = false;
                isTiming = false;
                
            }
                
        
        }

        //NOTE: base.Interact() calls Interact() is from the ContainerInteractable class
        //This updates the heldItem and storedItem variables in memory
    }

    
    //Starts the timer in Update()
    public void StartTimer()
    {
        timer = timerDuration;
        isTiming = true;
        Debug.Log("The food began to cook");
    }

    
    new void Update()
    {

        //Starts cooking the storedItem
        if (isTiming)
        {
            timer -= Time.deltaTime;

            // Print the remaining whole seconds
            Debug.Log($"Time left: {Mathf.CeilToInt(timer)}s");

            if (timer <= 0f)
            {
                cook(); 
            }

            //The player didn't grab the object, so it burns
            if(timer <= burnTime && burnedObject != null){
                
                
                GameObject burnedObjectSpawned = Instantiate(burnedObject, storedItem.position, storedItem.rotation);
                GameObject itemToEliminate = storedItem.gameObject;
                storedItem = burnedObjectSpawned.transform;
                Destroy(itemToEliminate);

                //Disabling the physics and object rotation of the burn
                storedItem.transform.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + storedItem.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
                storedItem.transform.SetParent(transform, true);
                storedItem.GetComponent<Rigidbody>().isKinematic = true;
                storedItem.GetComponent<Collider>().isTrigger = true;

                Debug.Log("Your food burned");


                isTiming = false;
            }
        }

        UpdateOutline();
    }

    //When the timer = 0 in Update(), Instantiates the item of the recipe, and destroys the stored item.
    
    void cook(){
       
        if(findInputItemInRecipeList(storedItem.GetComponent<GrabInteractable>().itemType) != -1){
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
