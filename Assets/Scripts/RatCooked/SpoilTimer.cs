using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpoilTimer : MonoBehaviour
{
    /*

    custom script trait to be added to food prefabs:

    if(this.gameObject isTouching getobject(floor)) //note for self: look at the xr stuff and see how they do it
    {
    activate a custom timer giannisakritidis.com/blog/Unity-Timers/
    (Timer should pause when food is picked up)
    listen.(eventsthatmightinfluencetimer) POTENTIAL ISSUE: becuase the if statement depends on if its touching the floor
    cant activate the timers deactivation
    after timer ends: SummonRat().Invoke (use events cause it decouples)
    }


    rats listen in on the signal of spoiled food */
    
    
    public event Action OnSummonedRat;
    
    [Tooltip("bool for food touching the ground")]
    public bool OnGround = false;
    [Tooltip("kinda only matters if the ground is ever not flat but here just in case")]
    public float GroundedOffset = -0.14f;
    [Tooltip("sphere radius for the grounded check")]
    public float GroundedRadius = 0.5f;
    [Tooltip("layer for checking the ground")]
    public LayerMask GroundLayers;

    [Tooltip("time it takes for a rat to spawn")]
    [SerializeField] private float RatTimerMax = 5f;
    private float CurrentRatTimer = 5f;

    private bool TimerOn = false; //used so that multiple timers don't happen
    private bool TimerEnded = false;


    
    private void MainProcess()
    {
       if (OnGround && !TimerOn)
       {
           TimerOn = true; //tries to only do one timer, potential bug: mutliple timers
           CurrentRatTimer = RatTimerMax; //resets the timer to the given max
       } 
       else if (TimerOn)
       {
           RatTimer();
       }
       else if (TimerEnded)
       {
           SummonRat();
       }
    }

    private void Awake()
    {
        GrabInteractable interactable = GetComponent<GrabInteractable>();
        if (interactable != null && interactable.itemType < ItemType.TOMATO) //if the interactable is assigned and the item type is less than tomato (the first "edible" item type)
        {
            this.enabled = false; //disables the script, because its not an edible object
        }
    }

    private void Update()
    {
        GroundedCheck();
        MainProcess();
    }

    private void RatTimer()
    {
        if (OnGround) //personal item timer only ticks down when still on the ground, preserves the timer and pauses it otherwise
        {
            CurrentRatTimer -= Time.deltaTime; //timer ticking down
        }
        
        if (CurrentRatTimer <= 0) //end condition, duhh
        {
            TimerEnded = true;
            TimerOn = false;
            CurrentRatTimer = 0;
        }
    }

    private void SummonRat()
    {
        Debug.Log("Spoiled food timed out: Summon rat called"); 
        OnSummonedRat?.Invoke();
        TimerEnded = false;
    }

    private void GroundedCheck()
    {
        //first creates a sphere at the transforms position (basically at the foods position)
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        //then checks if the sphere is touching a grounded layer using the position and radius, sets OnGround to true if touching a grounded layer
        OnGround = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
    
    
}
