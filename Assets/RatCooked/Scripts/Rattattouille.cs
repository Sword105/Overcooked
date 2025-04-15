using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Ratattouille : MonoBehaviour //RATMANAGER
{
    public static Ratattouille Instance { get; private set; } //instance accessible anywhere
    
    [SerializeField] private GameObject RatPrefab;
    [SerializeField] private List<Transform> RatSpawnLocations;
    [SerializeField] private List<Transform> RatEscapeLocations;
    [SerializeField] private List<GameObject> RatPrefabs;
    private List<RatLogic> activeRats = new List<RatLogic>();
    
    private List<SpoilTimer> timers = new List<SpoilTimer>(); //creates a reference to a list of all spoil timer objects in the scene

    public Transform PassEscapeLocation() //kinda shitty code but tbh i cant think of an easy way to reference this to the rats
    {
        int escapeIndex = UnityEngine.Random.Range(0, RatEscapeLocations.Count);
        Transform chosenLocation = RatEscapeLocations[escapeIndex];
        return chosenLocation;
    }
    public void RegisterSpoilTimer(SpoilTimer ratTimer) //must be public
    {
        ratTimer.OnSummonedRat += (Transform target) => SpawnRat(target); //listener for the rat summon event by the timer
    }
    
    private void Awake()
        {
            Instance = this;
        }
    private void SpawnRat(Transform target)
    {
        Transform chosenLocation = FindLocation(); 
        
        GameObject rat = Instantiate(RatPrefab, chosenLocation.position, Quaternion.identity);
        RatLogic ai = rat.GetComponent<RatLogic>();
        
        if (ai != null)
        {
            Debug.Log("rat given directions");
            ai.SetTarget(target); //basically just sets its first destination to the food that spawned it
        }
        
        Debug.Log("Rat spawned at: " + chosenLocation.position + ", heading to " + target.position);

    }

    private Transform FindLocation() //essentially picks a random location from the list of existing spawn points for variety
    {
        return RatSpawnLocations[Random.Range(0, RatSpawnLocations.Count)];
    }

}



/*
event ratpocalypse
(summon in a fuck ton of rats)
*/



//------------------------CODE AND IDEAS GRAVEYARD----------------------------



/*private void Start() //Only here as a failsafe if food is prespawned in the scene
    {
        foreach (SpoilTimer timer in FindObjectsOfType<SpoilTimer>()) //finds all objects with spoiltimer
        {
            RegisterSpoilTimer(timer); //and registers them to the list
        }
    }*/



//IDEA: if (spoiled food signal invoked)
////grab a random waiting rat (if there are no more waiting rats, spawn one in, saves memory, uses preexisting ones first)

/*public static event summonRat //would go on the level or game manager
{
    int ratCounter = 0; //must be persistently tracked throughout the level
    if (ratCounter < 11) //if the counter is less than the rats pre-placed in level
    {
        findRat(ratCounter); //find one of those pre-placed rats and use them
    }
    else
    {
        spawnRat(); //otherwise, create a new one (basically prefers cheaper method over spawning one in directly)
    }

    void findRat(int ratCounter)
    {
        if (rat[ratCounter] == Passive)
        {
            set ratstate for rat[ratCounter] as Active //POTENTIAL PROBLEM: removing a rat coutner and calling a rat after might bug out, might need a queue system
        }
        else
        {
            findRat(++ratCounter); //recursively call the method until a rat is found, here as a failsafe
        }
    }*/
 