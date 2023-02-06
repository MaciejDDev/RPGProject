using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    const int GENERAL_SIZE = 9;
    public ItemSlot[] GeneralInventory = new ItemSlot[GENERAL_SIZE];

    [SerializeField] Item _debugItem;


    private void Awake()
    {
        for (int i = 0; i < GENERAL_SIZE; i++)
        {
            GeneralInventory[i] = new ItemSlot();
        }
    }
    public void AddItem(Item item)
    {
        var firstAvailableSlot = GeneralInventory.FirstOrDefault(t => t.IsEmpty);
        firstAvailableSlot.SetItem(item);
    }
    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        var lastItem = GeneralInventory.Last()._item;
        for (int i = GENERAL_SIZE - 1; i > 0; i--)
        {
            GeneralInventory[i].SetItem(GeneralInventory[i - 1]._item);
        }
        GeneralInventory.First().SetItem(lastItem);
    }
}
