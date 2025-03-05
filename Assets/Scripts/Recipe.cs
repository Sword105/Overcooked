using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Recipe class to create a recipe using two Food objects
public class Recipe : MonoBehaviour
{

    //A recipe consist on two Food objects, the result of the combination, and a name for that recipe.
     public Food food1;
    public Food food2;
    public Food result;
    public string recipeName;


    //This may be useful in the future, returns true if two given Food objects are part of the recipe
    public bool Matches(Food f1, Food f2) {
        return (this.food1.name == f1.name && this.food2.name == f2.name) ||
               (this.food1.name == f2.name && this.food2.name == f1.name);
    }
}
