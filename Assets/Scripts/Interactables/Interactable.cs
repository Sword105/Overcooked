using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutlineThing))]
public abstract class Interactable : MonoBehaviour
{
    private bool eventCheck = false;
    //Placeholder method
    //This is meant to be overridden by subclasses of Interactable
    void Start()
    {
        this.GetComponent<OutlineThing>().OutlineWidth = 5;
        this.GetComponent<OutlineThing>().enabled = false;
        PlayerEvent.sendPlayerData += HandlePlayerData;
    }
    
    public void HandlePlayerData(Collider x)
    {
        if (!eventCheck)
        {
            if (x.gameObject == null)
            {
                this.GetComponent<OutlineThing>().enabled = false;
            }
            else
            {
                Debug.Log(x.gameObject.name);
                if (ReferenceEquals(x.gameObject, this.gameObject))
                {
                    this.GetComponent<OutlineThing>().enabled = true;
                }
                else
                {
                    this.GetComponent<OutlineThing>().enabled = false;
                }
            }
            eventCheck = true;
        }
        else
        {
            eventCheck = false;
        }
    }

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
