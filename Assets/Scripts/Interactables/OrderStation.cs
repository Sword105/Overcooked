using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStation : StorageContainer
{
    public LevelManager levelManager;

    public override void Interact(GameObject player, Transform heldItem)
    {
        if (heldItem.GetComponent<PlateInteractable>() != null)
        {
            foreach (Order x in levelManager.orders)
            {
                bool foundItem;
                bool correctOrder = true;

                if (heldItem.GetComponent<PlateInteractable>().foodStored.Count != x.order.Count)
                {
                    continue;
                }

                foreach (ItemType y in x.order)
                {
                    foundItem = false;
                    for (int i = 0; i < heldItem.GetComponent<PlateInteractable>().foodStored.Count; i++)
                    {
                        if (y == heldItem.GetComponent<PlateInteractable>().foodStored[i])
                        {
                            foundItem = true;
                        }
                    }
                    if (!foundItem)
                    {
                        correctOrder = false;
                        break;
                    }
                }

                if (correctOrder)
                {
                    Debug.Log("Found order");
                    levelManager.CompleteOrder(x);

                    base.Interact(player, heldItem);
                    Destroy(heldItem.GetComponent<Interactable>());

                    break;
                }
            }
        }
    }
}
