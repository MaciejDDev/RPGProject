using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public Sprite Icon;

    [Multiline(lines:3)]
    public string Description;


    [ContextMenu("Add 1")]
    public void Add1() => Inventory.Instance.AddItem(this);
    [ContextMenu("Add 5")]
    public void Add5()
    {
        for (int i = 0; i < 5; i++)
            Inventory.Instance.AddItem(this);
    }
    [ContextMenu("Add 10")]
    public void Add10()
    {
        for (int i = 0; i < 10; i++)
            Inventory.Instance.AddItem(this);
    }
}
