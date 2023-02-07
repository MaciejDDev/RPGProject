using System;

[Serializable]
public class ItemSlot
{
    public Item _item;
    SlotData _slotData;

    public bool IsEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
    }

    internal void Bind(SlotData slotData)
    {
        _slotData = slotData;
        //SetItem();
    }
}

[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}