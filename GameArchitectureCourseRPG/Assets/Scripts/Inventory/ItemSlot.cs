using System;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    SlotData _slotData;

    public Item Item;
    public event Action Changed;
    public bool IsEmpty => Item == null;

    public bool HasStackSpaceAvailable => _slotData.StackCount < Item.MaxStackSize;

    public int StackCount => _slotData.StackCount;

    public void SetItem(Item item)
    {
        var previousItem = Item;
        Item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
        _slotData.StackCount = 1;
        if(previousItem != Item)
        {
            Changed?.Invoke();
        }

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
        slotToSwapWith.SetItem(Item);
        SetItem(itemInOtherSlot);
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    public void ModifyStack(int amount)
    {
        _slotData.StackCount += amount;
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