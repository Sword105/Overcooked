using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratattouille : MonoBehaviour
{
    
    private List<SpoilTimer> timers = new List<SpoilTimer>(); //creates a reference to a list of all spoil timer objects in the scene

    private void Start()
    {
        SpoilTimer[] spoilTimers = FindObjectOfType<SpoilTimer>();
        
        foreach ()
        
        
    }
    
    private void SpawnRat()
    {
        public GameObject RatPrefab;
    }

}


/*
event ratpocalypse
(summon in a fuck ton of rats)
*/



//------------------------CODE AND IDEAS GRAVEYARD----------------------------


//IDEA: if (spoiled food signal invoked)
////grab a random waiting rat (if there are no more waiting rats, spawn one in, saves memory, uses preexisting ones first)

/*public static event summonRat //would go on the level or game manager
{
    int ratCounter = 0; //must be persistently tracked throughout the level
    if (ratCounter < 11) //if the counter is less than the rats pre-placed in level
    {
        findRat(ratCounter); //find one of those pre-placed rats and use them
    }
    else
    {
        spawnRat(); //otherwise, create a new one (basically prefers cheaper method over spawning one in directly)
    }

    void findRat(int ratCounter)
    {
        if (rat[ratCounter] == Passive)
        {
            set ratstate for rat[ratCounter] as Active //POTENTIAL PROBLEM: removing a rat coutner and calling a rat after might bug out, might need a queue system
        }
        else
        {
            findRat(++ratCounter); //recursively call the method until a rat is found, here as a failsafe
        }
    }*/
 