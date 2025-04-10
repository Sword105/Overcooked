using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Timeline;
using TMPro;

[System.Serializable]
public struct Order
{
    public GameObject customer;
    public List<ItemType> order;
}

public class LevelManager : MonoBehaviour
{

    [SerializeField] private SpoilTimer ratSpawner; //reference to spoil timer for later event subscribing
    
    
    public float maxTimeInSeconds = 300f;
    public float timeForPlateRespawn = 10f;
    public float customerSpawnRate = 20f;
    private bool timeEnded = false;
    private float currentTimeLeft;

    public Transform customerSpawnPoint;
    public GameObject customerPrefab;

    public List<Order> orders;
    
    public PlateStation plateStation;
    public OrderStation orderStation;
    public LevelMenu menu;


    private GameObject currentExitingNPC;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeLeft = maxTimeInSeconds;
        StartCoroutine(SpawnCustomer(customerSpawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        TimeTick();
    }

    public TextMeshProUGUI text;
    void TimeTick()
    {
        if (!timeEnded)
        {            
            currentTimeLeft -= Time.deltaTime;
            int seconds = ((int)currentTimeLeft % 60);
            int minutes = ((int)currentTimeLeft / 60);
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            text.text = formattedTime + " left";


            if (currentTimeLeft <= 0)
            {
                timeEnded = true;
                Debug.Log("Clock ended");
            }
        }
    }

    public void CompleteOrder(Order order)
    {
        currentExitingNPC = order.customer;
        currentExitingNPC.GetComponent<NavMeshAgent>().SetDestination(orderStation.transform.position);
        Invoke("CustomerOrderPickup", 2f);
        orders.Remove(order);
        plateStation.Invoke("AddPlate", timeForPlateRespawn);
    }

    public IEnumerator SpawnCustomer(float spawnRate)
    {
        yield return new WaitForSeconds(5f);
        while (!timeEnded)
        {
            int numberOfCustomers = Random.Range(1, 4);

            for (int i = 1; i <= numberOfCustomers; i++)
            {
                Order newOrder = new Order();
                newOrder.order = menu.RandomMenuItem();
                newOrder.customer = Instantiate(customerPrefab, customerSpawnPoint);

                newOrder.customer.GetComponent<NavMeshAgent>().SetDestination(plateStation.transform.position);
                orders.Add(newOrder);

                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(spawnRate + (5 * numberOfCustomers));
        }
    }

    public void CustomerOrderPickup()
    {
        Transform itemToPickUp = orderStation.orderToPickUp;
        itemToPickUp.rotation = Quaternion.identity;
        itemToPickUp.position = currentExitingNPC.transform.position + currentExitingNPC.transform.forward * 1.2f;
        itemToPickUp.SetParent(currentExitingNPC.transform, true);
        CustomerLeave();
        
        orderStation.orderToPickUp = null;
        currentExitingNPC = null;
    }

    public void CustomerLeave()
    {
        currentExitingNPC.GetComponent<NavMeshAgent>().SetDestination(customerSpawnPoint.transform.position);
        Destroy(currentExitingNPC, 3f);
    }
}
