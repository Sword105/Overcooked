using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public struct Order
{
    public GameObject NPC;
    public List<ItemType> order;
}

public class LevelManager : MonoBehaviour
{
    public float maxTimeInSeconds = 300f;
    public float timeForPlateRespawn = 10f;
    private bool timeEnded = false;
    private float currentTimeLeft;

    public List<Order> orders;
    public PlateStation plateStation;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeLeft = maxTimeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        TimeTick();
    }

    void TimeTick()
    {
        if (!timeEnded)
        {            
            currentTimeLeft -= Time.deltaTime;
            Debug.Log(currentTimeLeft + " seconds left");

            if (currentTimeLeft <= 0)
            {
                timeEnded = true;
                Debug.Log("Clock ended");
            }
        }
    }

    public void CompleteOrder(Order order)
    {
        Debug.Log("Tell NPC to pathfind");
        orders.Remove(order);
        plateStation.Invoke("AddPlate", timeForPlateRespawn);
    }
}
