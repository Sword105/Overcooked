using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerContainerUI : MonoBehaviour
{
    public RectTransform numberUI;
    public Transform TimedObjectPosition;
    public Vector3 worldOffset = new Vector3(0,2f,0);
    public Camera mainCamera;
    public TextMeshProUGUI text;
    TimedContainerInteractable timedContainerInteractable;
    void Start()
    {
        timedContainerInteractable = TimedObjectPosition.GetComponent<TimedContainerInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = TimedObjectPosition.position + worldOffset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        numberUI.position = screenPosition;
        int timerNum = (int)timedContainerInteractable.timer;
        string newText = timerNum.ToString();
        if (timedContainerInteractable.storedItem != null)
        {
            if (timerNum > 0)
            {
                text.text = newText;
            }
            else if (timerNum == 0)
            {
                text.text = "Done!";
            }
        }
        else
        {
            text.text = " ";
        }

        
    }
}
