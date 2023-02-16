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
    public List<ItemSlot> OverflowSlots= new List<ItemSlot>();
    
    [SerializeField] Item _debugItem;
    List<SlotData> _slotDatas;

    public static Inventory Instance { get; private set; }
    public ItemSlot TopOverflowSlot => OverflowSlots?.FirstOrDefault();

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

    bool AddItemToSlots(Item item, IEnumerable<ItemSlot> slots)
    {
        var stackableSlot = slots.FirstOrDefault(t => t.Item == item && t.HasStackSpaceAvailable);
        if (stackableSlot != null)
        {
            stackableSlot.ModifyStack(1);
            return true;
        }

        var slot = slots.FirstOrDefault(t => t.IsEmpty);
        if(slot != null)
        {
            slot.SetItem(item);
            return true;
        }
        return false;
    }

    public void AddItem(Item item, InventoryType preferredInventoryType = InventoryType.General)
    {
        var preferredSlots = preferredInventoryType == InventoryType.General ? GeneralSlots : CraftingSlots;
        var backupSlots = preferredInventoryType == InventoryType.General ? CraftingSlots : GeneralSlots;


        if (AddItemToSlots(item, preferredSlots))
            return;
        if (AddItemToSlots(item, backupSlots))
            return;
        if (AddItemToSlots(item, OverflowSlots))
            CreateOverFlowSlot();
        else
            Debug.LogError($"Unable to add item {item.name} to any slot because inventory is all full and overflow is not working");
        /*var firstAvailableSlot = preferredSlots.FirstOrDefault(t => t.IsEmpty);
        if (firstAvailableSlot == null)
        {
            firstAvailableSlot = backupSlots.FirstOrDefault(t => t.IsEmpty);
        }
        if (firstAvailableSlot == null)
        {
            firstAvailableSlot = OverflowSlots.Last();
            CreateOverFlowSlot();
            
        }
        if (firstAvailableSlot != null)
        {
            firstAvailableSlot.SetItem(item);
        }
         */
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
        _slotDatas = slotDatas;
        CreateOverFlowSlot();

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

    private void CreateOverFlowSlot()
    {
        
        var overflowSlot = new ItemSlot();
        var overflowSlotData = new SlotData() { SlotName = "Overflow" + OverflowSlots.Count };
        _slotDatas.Add(overflowSlotData);
        overflowSlot.Bind(overflowSlotData);
        OverflowSlots.Add(overflowSlot);
    }

    public void ClearCraftingSlots()
    {
        foreach (var slot in CraftingSlots)
            slot.RemoveItem();

    }

    public void RemoveItemFromSlot(ItemSlot itemSlot)
    {
        itemSlot.RemoveItem();
        if (itemSlot == TopOverflowSlot )
        {
            MoveOverflowItemsUp();
        }
    }

    void MoveOverflowItemsUp()
    {
        for (int i = 0; i < OverflowSlots.Count - 1; i++)
        {
            var item = OverflowSlots[i + 1].Item;
            OverflowSlots[i].SetItem(item);
        }
        OverflowSlots.Last().RemoveItem();
    }

    public void Swap(ItemSlot sourceSlot, ItemSlot targetSlot)
    {
        if (targetSlot == TopOverflowSlot)
            Debug.LogError("You can't drag items onto the overflow inventory");
        else if (sourceSlot == TopOverflowSlot)
            MoveItemFromOverflowSlot(targetSlot);
        else if (targetSlot != null && 
                 targetSlot.IsEmpty && 
                 Input.GetKey(KeyCode.LeftShift) && 
                 sourceSlot.StackCount > 1)
        {
            targetSlot.SetItem(sourceSlot.Item);
            sourceSlot.ModifyStack(-1);
        }
        else if (targetSlot != null &&
                 targetSlot.Item == sourceSlot.Item &&
                 targetSlot.HasStackSpaceAvailable)
        {
            int numberToMove = Mathf.Min(targetSlot.AvailableStackSpace, sourceSlot.StackCount);
            
            if (Input.GetKey(KeyCode.LeftShift) && numberToMove > 1)
                numberToMove = 1;
            
            targetSlot.ModifyStack(numberToMove);
            sourceSlot.ModifyStack(-numberToMove);
        }
        else
            sourceSlot.Swap(targetSlot);
    }

    void MoveItemFromOverflowSlot(ItemSlot targetSlot)
    {
        targetSlot.SetItem(TopOverflowSlot.Item);
        MoveOverflowItemsUp();
    }
}

public enum InventoryType
{
    General, 
    Crafting
}