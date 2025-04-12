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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        state = RatState.ACTIVE; //sets default state
    }

    public void SetTarget(Transform newTarget) //how to change destination
    {
        target = newTarget;
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            RatState state = RatState.SCURRY;
            Debug.Log("Rat heading to " + target.position);
            Debug.Log(state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
