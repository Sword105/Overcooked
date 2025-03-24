using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratattouille : MonoBehaviour
{

    /*
     
    custom trait to be added to food types: 
    
    if(this.gameObject isTouching getobject(floor))
    {
    activate a custom timer giannisakritidis.com/blog/Unity-Timers/
    Timer should pause when food is picked up
    listen.(eventsthatmightinfluencetimer) POTENTIAL ISSUE: becuase the if statement depends on if its touching the floor
    cant activate the timers deactivation
    after timer ends: SummonRat().Invoke (use events cause it decouples)
    }



    rats listen in on the signal of spoiled food

    //IDEA: if (spoiled food signal invoked)
    //grab a random waiting rat (if there are no more waiting rats, spawn one in, saves memory, uses preexisting ones first)
    
    
    public static event 
    
    int ratCounter = 0; //must be persistently tracked throughout the level
    if (ratCounter < 11) //if the counter is less than the rats pre-placed in level
    {
        findRat(ratCounter); //find one of those pre-placed rats and use them
    }
    else 
    {
        spawnRat() //otherwise, create a new one
    }
        
    findRat(int ratCounter) {
        if (rat[ratCounter] == Passive) 
        {
            set ratstate for rat[ratCounter] as Active //POTENTIAL PROBLEM: removing a rat coutner and calling a rat after might bug out, might need a queue system
        }
        else
        {
            findRat(++ratCounter)
        }
    }

    Rat States
    { 
    Passive (waiting to be activated off screen, does nothing), Active (Main Default State), Scurry (going to food),
    Eating (standing at food), 
    }


    event ratpocalypse
    (summon in a fuck ton of rats)

    */
}
