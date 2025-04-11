using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatStates : MonoBehaviour
{
    public enum RatState
    {
        PASSIVE = 0 /*(waiting to be activated off screen, does nothing)*/,
        ACTIVE = 1 /*(Main Default Managing State)*/, 
        SCURRY = 2 /*(going to food)*/,
        EATING = 3 /*(standing at food)*/, 
        GRABBED = 4 /*(being held by a user, possibly also used when thrown)*/,
        PUSHING = 5 /*(annoying bastard pushing off stuff from counter)*/,
        ESCAPING = 6 /*(running away with food or not, sets itself as passive at the end)*/
    }
}
