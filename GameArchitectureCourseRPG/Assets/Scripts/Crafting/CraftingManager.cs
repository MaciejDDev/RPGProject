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
        foreach (var recipe in _recipes)
        {
            if(IsMatchingRecipe(recipe, Inventory.Instance.CraftingSlots))
            {
                Inventory.Instance.ClearCraftingSlots();

                foreach (var reward in recipe.Rewards)
                    Inventory.Instance.AddItem(reward);
                
                Debug.Log($"Crafted the recipe {recipe.name}");
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
        for (int i = recipe.Ingredients.Count; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i].IsEmpty == false)
                return false;

        }
        return true;
    }
}
