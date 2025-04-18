using UnityEngine;
using UnityEngine.AI;

public class NavMeshStopOnRaycast : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public float raycastDistance = 10f;
    public LayerMask obstacleLayer;

    void Update()
    {
        // Send the ray forward from the agent
        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, obstacleLayer))
        {
            // Something is in the way — stop the agent
            agent.isStopped = true;
            Debug.Log("Raycast hit something. Stopping NavMeshAgent.");
        }
        else
        {
            // Nothing in the way — move to destination
            agent.isStopped = false;
            
        }
    }
}
