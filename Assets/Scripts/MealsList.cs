using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealsList : MonoBehaviour
{

    //An ArrayList that contains some food items
     public List<Food> foodItems = new List<Food>();

   

    //Find food in the ArrayList foodItems
    public Food FindFood(string foodName)
    {
        return foodItems.Find(food => food.name == foodName);
    }
}
