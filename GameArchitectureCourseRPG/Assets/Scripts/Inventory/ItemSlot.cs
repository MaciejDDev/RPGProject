using System;

[Serializable]
public class ItemSlot
{
    public Item _item;

    public bool IsEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
    }
}