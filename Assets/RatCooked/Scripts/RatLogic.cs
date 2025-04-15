using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class RatLogic : MonoBehaviour
{
    public event Action OnRatEating;
    public event Action OnRatEscaped;
    
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
    [SerializeField] float distanceThreshold = 0.8f;
    [SerializeField] bool isMoving = false;
    [SerializeField] float eatingTimerMax = 4f;
    [SerializeField] private Ratattouille ratManager;
    [SerializeField] float nearbyRadius = 2.5f; //float for the food scan
    private float currentEatingTimer = 4f;
    private bool timerEnded = false; 
    private bool isEscaping = false;
    private bool escapeStarted = false;
    private bool isGrabbed = false;
    private bool wasGrabbed = false;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        state = RatState.ACTIVE; //sets default state
    }

    private void Start()
    {
        currentEatingTimer = eatingTimerMax;
        
        if (ratManager == null)
        {
            ratManager = Ratattouille.Instance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceThreshold);
    }
    public void SetTarget(Transform newTarget) //how to change destination
    {
        target = newTarget;
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log("Rat heading to " + target.position);
            Debug.Log("Set target: " + state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("Untagged")) //IF THE RAT GETS GRABBED
        {
            agent.isStopped = true;
            Debug.Log("rat grabbed");
            state = RatState.GRABBED;
            wasGrabbed = true;
        }

        if (transform.CompareTag("Grabbable") && wasGrabbed) //IF THE RAT IS NO LONGER GRABBED
        {
            agent.isStopped = false;
            state = RatState.ESCAPING;
        }
        
        switch (state)
        {
            case RatState.ACTIVE:
                if (agent.velocity.sqrMagnitude > 0.1f) //isMoving
                {
                    state = RatState.SCURRY;
                }
                break;
            case RatState.SCURRY:
                StateScurry();
                break;
            case RatState.EATING:
                StateEating();
                break;
            case RatState.GRABBED:
                //cry about it
                break;
            case RatState.PUSHING:
                //spawns on a counter
                //pushes pots and pans off 
                break;
            case RatState.ESCAPING:

                if (!escapeStarted)
                {
                    SetTarget(ratManager.PassEscapeLocation());
                    escapeStarted = true;
                    break; //end the case early to stop bugs
                }
                

                if (CompareSelfVSTarget() && escapeStarted)
                {
                    OnRatEscaped?.Invoke(); //If reached escape point, invoke rat escaped event (prolly used for ratpocalypse and pushing, rat escape counter?) POSSIBLE bug goes off multiple times cause of frames
                    agent.isStopped = true;
                    Debug.Log("rat obliterated");
                    Destroy(gameObject); //RAT DESTROYED!!!!
                    
                }
                break;
        }
    }

    private void StateScurry()
    {
        Debug.Log(transform.position + " " + target.position);//CURRENT BUG, USES OLD TARGETS POSITION FOR GUIDE, COMPARES AGAINST CURRENT TARGET POSITION? I THINK?
        if (CompareSelfVSTarget()) //when reaching target... 
        {
            Debug.Log("Rat at " + target.position + ", switching to eating");
            SetTarget(transform);
            state = RatState.EATING;
            Debug.Log(state);
        }
    }

    private void StateEating()
    {
        if (CompareSelfVSTarget() && !timerEnded) //personal item timer only ticks down when the rat and item are still close by, preserves the timer and pauses it otherwise
        {
            OnRatEating?.Invoke(target); //currentEatingTimer -= Time.deltaTime; //timer ticking down
        }

        if (currentEatingTimer <= 0) //end condition, duhh
        {
            Debug.Log("eating over, switching to escaping");
            timerEnded = true;
            currentEatingTimer = 0;
            //foodEaten()//delete food prefab 
            
            if (TryFindNearbyFood(out var newTarget)) //if it found a nearby food
            {
                state = RatState.SCURRY;
                SetTarget(newTarget); //go to that one and do it all over again
                currentEatingTimer = eatingTimerMax;
                timerEnded = false;
            }
            else
            {
                state = RatState.ESCAPING; //otherwise, run away
            }
        }
    }

    private bool CompareSelfVSTarget() //compare distance between rat and target location
    {
        Debug.Log("comparing self");
        return Vector3.Distance(transform.position, target.position) <= distanceThreshold;
    }
    
    private bool TryFindNearbyFood(out Transform newTarget)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, nearbyRadius);

        foreach (var hit in hits)
        {
            SpoilTimer food = hit.GetComponent<SpoilTimer>();
            if (food != null)
            {
                newTarget = food.transform; //if it found something, newtarget is made the foods transform, and returns true
                return true;
            }
        }
        newTarget = null;
        return false;
    }

}
