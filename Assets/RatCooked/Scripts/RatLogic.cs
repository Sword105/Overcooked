using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatLogic : MonoBehaviour
{
    enum RatState
    {
        //PASSIVE = 0 /*(waiting to be activated off screen, does nothing)*/,
        ACTIVE = 0 /*(Main Default Managing State)*/, 
        SCURRY = 1 /*(going to food)*/,
        EATING = 2 /*(standing at food)*/, 
        GRABBED = 3 /*(being held by a user, possibly also used when thrown)*/,
        PUSHING = 4 /*(annoying bastard pushing off stuff from counter)*/,
        ESCAPING = 5 /*(running away with food or not, sets itself as passive at the end)*/
    }
    
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] RatState state = RatState.ACTIVE;
    [SerializeField] float distanceThreshold = 0.5f;
    [SerializeField] bool isMoving = false;
    [SerializeField] float eatingTimerMax = 4f;
     private float currentEatingTimer = 4f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        state = RatState.ACTIVE; //sets default state
    }

    private void Start()
    {
        currentEatingTimer = eatingTimerMax;
    }

    public void SetTarget(Transform newTarget) //how to change destination
    {
        target = newTarget;
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log("Rat heading to " + target.position);
            Debug.Log(state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (grabbed)
        {
            state = RatState.GRABBED;
        }*/
        
        switch (state)
        {
            case RatState.ACTIVE:
                if (isMoving)
                {
                    RatState state = RatState.SCURRY;
                }
                break;
            case RatState.SCURRY:
                Debug.Log(transform.position + " " + target.position);
                if (Vector3.Distance(transform.position, target.position) <= distanceThreshold) //when reaching target...
                {
                    Debug.Log("Rat at " + target.position + ", switching to eating");
                    SetTarget(transform);
                    state = RatState.EATING;
                }
                break;
            case RatState.EATING:
                if (OnGround) //personal item timer only ticks down when still on the ground, preserves the timer and pauses it otherwise
                {
                    currentEatingTimer -= Time.deltaTime; //timer ticking down
                }
        
                if (currentEatingTimer <= 0) //end condition, duhh
                {
                    TimerEnded = true;
                    currentEatingTimer = 0;
                    //delete food prefab
                    state = RatState.ESCAPING;
                }
                break;
            case RatState.GRABBED:
                break;
            case RatState.PUSHING:
                break;
            case RatState.ESCAPING:
                //sets target lcoation to the spawn points used as escape points, follows same system
                //If reached escape point, invoke rat escaped event (prolly used for ratpocalypse and pushing)
                break;
        }
    }
}
