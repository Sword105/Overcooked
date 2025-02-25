using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food class to store the name of a food
public class Food : GrabInteractable
{
    // The name of the Food
    public string name;

    // Some meals may have a Recipe
    public Recipe recipe;
}

