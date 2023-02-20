using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
   
    
    public ItemSlot _itemSlot;

   
    
    public static PlacementManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    public void BeginPlacement(ItemSlot itemSlot)
    {
        if (itemSlot== null || itemSlot.Item == null || itemSlot.Item.PlaceablePrefab == null)
        {
            Debug.LogError("Unable to place because of null item or placeable");
            return;
        }

        _itemSlot = itemSlot;
        Debug.Log($"Started placing item {itemSlot.Item}");

        var placeable = Instantiate(itemSlot.Item.PlaceablePrefab);
        placeable.transform.SetParent(transform);

    }


}
