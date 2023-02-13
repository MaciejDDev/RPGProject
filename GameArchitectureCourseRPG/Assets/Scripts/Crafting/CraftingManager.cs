using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] Recipe[] _recipes;




    void OnValidate() => _recipes = Extensions.GetAllInstances<Recipe>();


    public void TryCrafting()
    {
        int itemsInCraftingInventory = Inventory.Instance.CraftingSlots.Count(t => t.IsEmpty == false);
        Debug.Log($"Trying to craft with {itemsInCraftingInventory} items.");

        foreach (var recipe in _recipes)
        {
            if(IsMatchingRecipe(recipe, Inventory.Instance.CraftingSlots))
            {
                Debug.Log($"Found the recipe {recipe.name}");
                //CraftRecipe
                return;
            }
        }
    }

    bool IsMatchingRecipe(Recipe recipe, ItemSlot[] craftingSlots)
    {
        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            if (recipe.Ingredients[i] != craftingSlots[i].Item)
                return false;
        }
        return true;
    }
}
