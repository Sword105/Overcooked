using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionContainerUI : MonoBehaviour
{
    public RectTransform numberUI;
    public Transform TimedObjectPosition;
    public Vector3 worldOffset = new Vector3(0,2f,0);
    public Camera mainCamera;
    public TextMeshProUGUI text;
    ActionContainerInteractable actionContainerInteractable;
    bool cancelCount = false;
    void Start()
    {
        actionContainerInteractable = TimedObjectPosition.GetComponent<ActionContainerInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = TimedObjectPosition.position + worldOffset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        numberUI.position = screenPosition;
        int timerNum = (int)actionContainerInteractable.remainingInteractions;
        string newText = timerNum.ToString();
        if (actionContainerInteractable.storedItem != null)
        {
            if (timerNum > 0 && !cancelCount)
            {
                text.text = newText;
            }
        }
        else
        {
            text.text = " ";
        }

        
    }
}
