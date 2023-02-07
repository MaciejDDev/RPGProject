using System;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    SlotData _slotData;

    public Item Item;
    public event Action Changed;
    public bool IsEmpty => Item == null;


    public void SetItem(Item item)
    {
        var previousItem = Item;
        Item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
        if(previousItem != Item)
        {
            Changed?.Invoke();
        }

    }

    public void Bind(SlotData slotData)
    {
        _slotData = slotData;
        var item = Resources.Load<Item>(path: "Items/" + _slotData.ItemName);
        SetItem(item);
        //SetItem();
    }

    public void Swap(ItemSlot slotToSwapWith)
    {
        var itemInOtherSlot = slotToSwapWith.Item;
        slotToSwapWith.SetItem(Item);
        SetItem(itemInOtherSlot);
    }
}

[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}