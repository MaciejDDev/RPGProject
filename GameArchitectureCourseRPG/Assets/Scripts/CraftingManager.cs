using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public void TryCrafting()
    {
        int itemsInCraftingInventory = Inventory.Instance.CraftingSlots.Count(t => t.IsEmpty == false);
        Debug.Log($"Trying to craft with {itemsInCraftingInventory} items.");
    }

}
