using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{

    public List<EquipmentSlotType> EquipmentSlotTypes;
    public Material EquipMaterial;
    public string ModelName;
    
    public Sprite Icon;

    [Multiline(lines:3)]
    public string Description;

    public int MaxStackSize;

    public Placeable PlaceablePrefab;

    public List<StatMod> StatMods;



    [ContextMenu("Add 1")] public void Add1() => Player.ActivePlayer.Inventory.AddItem(this);
    [ContextMenu("Add 5")] public void Add5() { for (int i = 0; i < 5; i++) { Add1(); } }
    [ContextMenu("Add 10")] public void Add10() { for (int i = 0; i < 10; i++) { Add1(); } }
}
