using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Recipe class to create a recipe using two Food objects
public class Recipe : MonoBehaviour
{
     public Food food1;
    public Food food2;
    public string recipeName;

    public Recipe(Food food1, Food food2, string recipeName) {
        this.food1 = food1;
        this.food2 = food2;
        this.recipeName = recipeName;
    }

    //This may be useful in the future, returns true if two given Food objects are part of the recipe
    public bool Matches(Food f1, Food f2) {
        return (this.food1.name == f1.name && this.food2.name == f2.name) ||
               (this.food1.name == f2.name && this.food2.name == f1.name);
    }
}
