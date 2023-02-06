using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> _items = new List<Item>();
    [SerializeField] Item _debugItem;

    public void AddItem(Item item)
    {
        _items.Add(item);
    }
    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);

}
    
