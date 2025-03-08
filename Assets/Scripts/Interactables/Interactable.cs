using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Placeholder method
    //This is meant to be overridden by subclasses of Interactable
    public virtual void Interact(GameObject player, Transform heldItem)
    {
        //Debug.Log(player.name + " is interacting with object " + item.name);
    }
    
    //This creates a blue box above any interactable object (debug purposes)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + new Vector3(0, 1, 0), new Vector3(0.2f, 0.2f, 0.2f));
    }
}
