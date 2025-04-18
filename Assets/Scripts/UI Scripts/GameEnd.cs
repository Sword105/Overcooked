using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public LevelManager levelManager;
    public bool gameEnd = false;
    public GameObject endMenuUI;
    public TextMeshProUGUI text;

    void Start()
    {
        endMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.timeEnded || gameEnd)
        {
            text.text = "Customers satisfied: " + levelManager.customersSatisfied;
            endMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameEnd = true;
        }
    }
}
