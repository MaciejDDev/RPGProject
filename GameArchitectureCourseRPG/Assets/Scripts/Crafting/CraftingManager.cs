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

        var itemsInCrafting = Inventory.Instance.CraftingSlots
            .Select(t => t.Item)
            .Where(t => t != null).ToList();

        foreach (var recipe in _recipes)
        {
            if (AreListsMatching(recipe.Ingredients, itemsInCrafting) == false)
                continue;

            var rewards = IsMatchingRecipe(recipe, Inventory.Instance.CraftingSlots) ? recipe.Rewards : recipe.FallbackRewards;
            
            Inventory.Instance.ClearCraftingSlots();

            foreach (var reward in rewards)
                Inventory.Instance.AddItem(reward, InventoryType.Crafting);
                
            Debug.Log($"Crafted the recipe {recipe.name}");
            return;

        }
    }

    bool AreListsMatching(List<Item> recipeIngredients, List<Item> itemsInCrafting)
    {
        if (recipeIngredients.Except(itemsInCrafting).Any())
            return false;
        if (itemsInCrafting.Except(recipeIngredients).Any())
            return false;
        return true;
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
