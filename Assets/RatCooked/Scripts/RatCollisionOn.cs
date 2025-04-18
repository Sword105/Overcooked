using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCollisionOn : MonoBehaviour
{
    public bool isSpawnZone = true; // toggle in Inspector
    public float fadeDuration = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        RatLogic rat = other.GetComponent<RatLogic>();
        if (rat == null) return;

        if (isSpawnZone)
        {
            rat.FadeIn(fadeDuration);
        }
        else
        {
            rat.FadeOut(fadeDuration);
        }
    }
}
