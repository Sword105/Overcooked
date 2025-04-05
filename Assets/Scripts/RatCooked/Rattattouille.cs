using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratattouille : MonoBehaviour
{

    

    //IDEA: if (spoiled food signal invoked)
    //grab a random waiting rat (if there are no more waiting rats, spawn one in, saves memory, uses preexisting ones first)
    
    
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
        }
        
        void spawnRat() 
        {
            public GameObject ratPrefab; //reference to the ratPrefab
        
            Instantiate(ratPrefab, findRatSpawn(), Quaternion.identity); //spawns in the ratPrefab after being given the map location at 0 rotation
        }
        
        void findRatSpawn() 
        {

        }
    }




        public enum RatStates
        {
        Passive /*(waiting to be activated off screen, does nothing)*///,
        //Active /*(Main Default Managing State)*///, 
        //Scurry /*(going to food)*///,
        //Eating /*(standing at food)*///, 
        //Grabbed /*(being held by a user, possibly also used when thrown)*///,
        //Pushing /*(annoying bastard pushing off stuff from counter)*///,
        //Escape /*(running away with food or not, sets itself as passive at the end)*/
        //}

/*
        event ratpocalypse
        (summon in a fuck ton of rats)
        */
}
