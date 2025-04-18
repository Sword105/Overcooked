using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class RatLogic : MonoBehaviour
{
    public event Action<RatLogic, SpoilTimer> OnRatEating;
    public event Action<RatLogic, SpoilTimer> OnRatStoppedEating;
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
    
    [SerializeField] public SpoilTimer Obsession;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private Ratattouille ratManager;
    [SerializeField] private RatState state = RatState.ACTIVE;
    [SerializeField] private float distanceThreshold = 0.8f;
    [SerializeField] private float nearbyRadius = 2.5f; //float for the food scan
    public bool finishedEating = false;
    public bool currentlyEating = false;
    private bool timerEnded = false; 
    private bool escapeStarted = false;
    private bool wasGrabbed = false;
    [SerializeField] float patienceMeter = 5f;
    [SerializeField] private float maxPatience = 5f;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        state = RatState.ACTIVE; //sets default state
        patienceMeter = maxPatience;
    }

    private void Start()
    {
        
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

    private void HandleFoodEaten(SpoilTimer food)
    {
        if (state == RatState.EATING)
        {
            finishedEating = true;
        }
    }
    public void SetObssession(SpoilTimer targetFood)
    {
        if (Obsession != null)
        {
            Obsession.OnFoodEaten -= HandleFoodEaten;
        }
        
        Obsession = targetFood;

        if (Obsession != null)
        {
            Obsession.OnFoodEaten += HandleFoodEaten;
        }
    }

    public void TargetObsession() //food specific targetting 
    {
        SetTarget(Obsession.transform);
    }
    public void SetTarget(Transform newTarget) //how to change destination
    {
        target = newTarget;
        if (agent != null && target != null)
        {
            agent.SetDestination(target.transform.position);
            //Debug.Log("Rat heading to " + target.transform.position);
            //Debug.Log("Set target: " + state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("Untagged")) //IF THE RAT GETS GRABBED (uses tag system, interactables start grabbable and turn off their tag when held)
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
                StateEscaping();
                break; //end the case early to stop bugs
        }
    }

    private void StateEscaping()
    {
        if (!escapeStarted)
        {
            escapeStarted = true;
            SetTarget(ratManager.PassEscapeLocation());
            return;
        }

        if (CompareSelfVSTarget() && escapeStarted)
        {
            escapeStarted = false;
            OnRatEscaped?.Invoke(); //If reached escape point, invoke rat escaped event (prolly used for ratpocalypse and pushing, rat escape counter?) POSSIBLE bug goes off multiple times cause of frames
            agent.isStopped = true;
            Debug.Log("rat obliterated");
            Destroy(gameObject); //RAT DESTROYED!!!!
        }
    }

    private void StateScurry()
    {
        Debug.Log(transform.position + " " + target.transform.position);//CURRENT BUG, USES OLD TARGETS POSITION FOR GUIDE, COMPARES AGAINST CURRENT TARGET POSITION? I THINK?
        if (CompareSelfVSTarget()) //when reaching target... 
        {
            Debug.Log("Rat at " + target.transform.position + ", switching to eating");
            SetTarget(transform); //potentially unneccessary code, i shure hope this doesnt kill me later
            state = RatState.EATING;
            Debug.Log(state);
        }
        else
        {
            patienceMeter -= Time.deltaTime;
        }

        if (patienceMeter <= 0f)
        {
            Debug.Log("impatient");
            SetTarget(target); //impatient
            patienceMeter = maxPatience;
        }
    }

    private void StateEating()
    {
        if (!finishedEating) //if hasnt finished......
        {
            if (CompareSelfVSTarget()) //...and the target is still nearby....
            {
                if (!currentlyEating) //.......and the bool hasnt been activated (so its not constantly calling an event)......
                { 
                    currentlyEating = true;
                   OnRatEating?.Invoke(this, Obsession); //invoke this event which will toggle the bool to tick down the items timer
                }
            }
            else //if the item isn't nearby, go to it
            {
                currentlyEating = false;
                OnRatStoppedEating?.Invoke(this, Obsession);
                SetTarget(Obsession.transform);
                
            }
        }

        if (finishedEating) //end condition, duhh
        {
            Debug.Log("eating over, switching to escaping");
            timerEnded = true;
            finishedEating = false;
            
            if (TryFindNearbyFood(out SpoilTimer newTarget)) //if it found a nearby food
            {
                state = RatState.SCURRY;
                SetObssession(newTarget);
                TargetObsession(); //go to that one and do it all over again
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
        Debug.Log(transform.position);
        Debug.Log(target.transform.position);
        Debug.Log((Vector3.Distance(transform.position, target.transform.position) <= distanceThreshold));
        return Vector3.Distance(transform.position, target.transform.position) <= distanceThreshold;
    }
    
    private bool TryFindNearbyFood(out SpoilTimer newTarget)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, nearbyRadius);

        foreach (var hit in hits)
        {
            SpoilTimer food = hit.GetComponent<SpoilTimer>();
            if (food != null && !food.isEaten)
            {
                newTarget = food; //if it found something, newtarget is made the foods transform, and returns true
                return true;
            }
        }
        newTarget = null;
        return false;
    }

}
