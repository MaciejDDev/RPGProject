using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    const int GENERAL_SIZE = 9;
    const int CRAFTING_SIZE = 9;
    
    public ItemSlot[] GeneralSlots = new ItemSlot[GENERAL_SIZE];
    public ItemSlot[] CraftingSlots = new ItemSlot[CRAFTING_SIZE];
    
    [SerializeField] Item _debugItem;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < GENERAL_SIZE; i++)
        {
            GeneralSlots[i] = new ItemSlot();
        }
        for (int i = 0; i < CRAFTING_SIZE; i++)
        {
            CraftingSlots[i] = new ItemSlot();
        }
    }
    public void AddItem(Item item, InventoryType preferredInventoryType = InventoryType.General)
    {
        var preferredSlots = preferredInventoryType == InventoryType.General ? GeneralSlots : CraftingSlots;
        var BackupSlots = preferredInventoryType == InventoryType.General ? CraftingSlots : GeneralSlots;
        
        
        var firstAvailableSlot = preferredSlots.FirstOrDefault(t => t.IsEmpty);
        if (firstAvailableSlot == null)
        {
            firstAvailableSlot = BackupSlots.FirstOrDefault(t => t.IsEmpty);
        }
        if (firstAvailableSlot != null)
            firstAvailableSlot.SetItem(item);
    }
    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        var lastItem = GeneralSlots.Last().Item;
        for (int i = GENERAL_SIZE - 1; i > 0; i--)
        {
            GeneralSlots[i].SetItem(GeneralSlots[i - 1].Item);
        }
        GeneralSlots.First().SetItem(lastItem);
    }

    public void Bind(List<SlotData> slotDatas)
    {
        for (int i = 0; i < GeneralSlots.Length; i++)
        {
            var slot = GeneralSlots[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "General" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "General" + i };
                slotDatas.Add(slotData);
            }

            slot.Bind(slotData);
        }
        for (int i = 0; i < CraftingSlots.Length; i++)
        {
            var slot = CraftingSlots[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "Crafting" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "Crafting" + i };
                slotDatas.Add(slotData);
            }

            slot.Bind(slotData);
        }

    }

    public void ClearCraftingSlots()
    {
        foreach (var slot in CraftingSlots)
            slot.RemoveItem();

    }
}

public enum InventoryType
{
    General, 
    Crafting
}