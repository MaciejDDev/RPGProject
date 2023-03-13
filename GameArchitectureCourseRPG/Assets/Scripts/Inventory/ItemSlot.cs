using System;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public ItemSlot() { }
    public ItemSlot(EquipmentSlotType equipmentSlotType)
    {
        EquipmentSlotType = equipmentSlotType;
    }

    SlotData _slotData;

    public Item Item;
    public event Action Changed;
    public bool IsEmpty => Item == null;
    public bool HasStackSpaceAvailable => _slotData.StackCount < Item.MaxStackSize;
    public int StackCount => _slotData.StackCount;
    public int AvailableStackSpace => this.Item != null ? Item.MaxStackSize - _slotData.StackCount : 0;
    public readonly EquipmentSlotType EquipmentSlotType;

    public void SetItem(Item item, int stackCount = 1)
    {
        var previousItem = Item;
        Item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
        _slotData.StackCount = stackCount;
        
        Changed?.Invoke();

    }

    public void Bind(SlotData slotData)
    {
        _slotData = slotData;
        var item = Resources.Load<Item>(path: "Items/" + _slotData.ItemName);

        Item = item;
        Changed?.Invoke();

        //SetItem(item);
        //SetItem();
    }

    public void Swap(ItemSlot slotToSwapWith)
    {
        var itemInOtherSlot = slotToSwapWith.Item;
        int stackCountInOtherSlot = slotToSwapWith.StackCount;

        slotToSwapWith.SetItem(Item, StackCount);
        SetItem(itemInOtherSlot, stackCountInOtherSlot);
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    public void ModifyStack(int amount)
    {
        _slotData.StackCount += amount;
        if (_slotData.StackCount <= 0)
            SetItem(null);
        else
            Changed?.Invoke();
    }
}

[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;

    public int StackCount;
}