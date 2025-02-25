using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesList : MonoBehaviour
{
    //An ArrayList that contains all the recipes
 public List<Recipe> recipes = new List<Recipe>();

    public void AddRecipe(Recipe recipe) {
        this.recipes.Add(recipe);
    }

    //This may be useful, finds a recipe given two Food objects
    public Recipe FindRecipe(Food f1, Food f2) {
        foreach (var recipe in this.recipes) {
            if (recipe.Matches(f1, f2)) {
                return recipe;
            }
        }
        return null;
}
}