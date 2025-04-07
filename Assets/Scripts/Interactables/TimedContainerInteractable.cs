using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedContainerInteractable : ContainerInteractable
{

    //public List<Recipe> recipeList;
    public float timerDuration = 5f; // total time in seconds
    private float timer;
    private bool isTiming = false;


    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem != null)
        {

            
            //If there is not a stored item and you are holding something, place it down on the table
            if (storedItem == null)
            {
                heldItem.transform.position = new Vector3(transform.position.x, transform.GetComponent<MeshRenderer>().bounds.max.y + heldItem.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z);
                heldItem.transform.SetParent(transform, true);
                base.Interact(player, heldItem);


                StartTimer();
            }

            else if (storedItem.GetComponent<GrabInteractable>().itemType == ItemType.PLATE)
            {
                storedItem.GetComponent<GrabInteractable>().Interact(player, heldItem);
            }
        }
        else
        {
            //If there is a stored item, and you aren't holding anything grab it
            if (storedItem != null)
            {
                storedItem.GetComponent<GrabInteractable>().Interact(player, heldItem);
            }
            base.Interact(player, heldItem);
        }

        //NOTE: base.Interact() calls Interact() is from the ContainerInteractable class
        //This updates the heldItem and storedItem variables in memory
    }


    
    //Starts the timer
    public void StartTimer()
    {
        timer = timerDuration;
        isTiming = true;
    }

    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;

            // Print the remaining whole seconds
            Debug.Log($"Time left: {Mathf.CeilToInt(timer)}s");

            if (timer <= 0f)
            {
                isTiming = false;
                Debug.Log("Timer finished!");
                cook();
            }
        }
    }

    //When the timer = 0, Instantiates the item of the recipe, and destroys the stored item.
    void cook(){
        if(storedItem != null){
            int inputRecipeList = findInputItemInRecipeList(storedItem.GetComponent<GrabInteractable>().itemType);

            GameObject cookedMeal = Instantiate(recipeList[inputRecipeList].outputGameObject, storedItem.position, storedItem.rotation);
            Destroy(storedItem.GetComponent<GameObject>());
            storedItem = cookedMeal.transform;

            
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
    

   /* This is another solution that I was trying to use for the timers, it is not completed
   
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

    */

}
