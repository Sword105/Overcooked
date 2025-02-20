using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(GameObject player, Transform heldItem)
    {
        //Debug.Log(player.name + " is interacting with object " + item.name);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + new Vector3(0, 1, 0), new Vector3(0.2f, 0.2f, 0.2f));
    }
}
