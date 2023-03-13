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
    public List<ItemSlot> EquipmentSlots = new List<ItemSlot>();
    
    [SerializeField] Item _debugItem;
    List<SlotData> _slotDatas;
    [SerializeField] EquipmentSlotType[] _allEquipmentSlotTypes;

    public static Inventory Instance { get; private set; }
    public ItemSlot TopOverflowSlot => OverflowSlots?.FirstOrDefault();


    void OnValidate() => _allEquipmentSlotTypes = Extensions.GetAllInstances<EquipmentSlotType>();
    void Awake()
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

        foreach (var slotType in _allEquipmentSlotTypes)
        {
            var slot = new ItemSlot(slotType);
            EquipmentSlots.Add(slot);
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


    public void AddItemFromEvent(Item item) => AddItem(item);

    public void AddItem(Item item, InventoryType preferredInventoryType = InventoryType.General)
    {
        var preferredSlots = preferredInventoryType == InventoryType.General ? GeneralSlots : CraftingSlots;
        var backupSlots = preferredInventoryType == InventoryType.General ? CraftingSlots : GeneralSlots;


        if (AddItemToSlots(item, preferredSlots))
            return;
        if (AddItemToSlots(item, backupSlots))
            return;
        if (AddItemToSlots(item, OverflowSlots))
        {
            if(OverflowSlots.Any(t => t.IsEmpty) == false)
                CreateOverFlowSlot();
        }
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
        BindToSlots(slotDatas, GeneralSlots, "General");
        BindToSlots(slotDatas, CraftingSlots, "Crafting");

        var overflowSlotDatas = slotDatas.Where(t => t.SlotName.StartsWith("Overflow") &&
                                String.IsNullOrWhiteSpace(t.ItemName) == false).ToList();

        foreach (var overflowSlotData in overflowSlotDatas)
        {
            var itemSlot = new ItemSlot();
            itemSlot.Bind(overflowSlotData);
            OverflowSlots.Add(itemSlot);
        }


        CreateOverFlowSlot();
        TopOverflowSlot.Changed += () =>
        {
            if (TopOverflowSlot.IsEmpty && OverflowSlots.Any(t => !t.IsEmpty))
                MoveOverflowItemsUp();
        };
        

    }

    static void BindToSlots( List<SlotData> slotDatas, ItemSlot[] slots,string  slotName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == slotName + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = slotName + i };
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

    public ItemSlot GetEquipmentSlot(EquipmentSlotType equipmentSlotType)
    {
        return EquipmentSlots.FirstOrDefault(t => t.EquipmentSlotType == equipmentSlotType);
    }
}

public enum InventoryType
{
    General, 
    Crafting
}