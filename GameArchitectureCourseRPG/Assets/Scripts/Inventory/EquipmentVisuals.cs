using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentVisuals : MonoBehaviour
{
    public List<EquipmentVisual> Visuals;



    void Start()
    {
        foreach (var visual in Visuals)
            visual.DefaultMaterial = visual.SkinnedMeshRenderers.FirstOrDefault()?.material;
        
        foreach (var slot in Inventory.Instance.EquipmentSlots)
        {
            slot.Changed += () => UpdateEquipmentVisual(slot);
        }
    }

    void UpdateEquipmentVisual(ItemSlot slot)
    {
        foreach (var visual in Visuals.Where(t => t.EquipmentSlotType == slot.EquipmentSlotType))
        {
            foreach (var skinnedMeshRenderer in visual.SkinnedMeshRenderers)
            {
                skinnedMeshRenderer.material = slot.Item?.EquipMaterial ?? visual.DefaultMaterial;
            }
        }
    }
}



[Serializable]
public class EquipmentVisual
{
    public EquipmentSlotType EquipmentSlotType;
    public List<SkinnedMeshRenderer> SkinnedMeshRenderers;
    public Material DefaultMaterial;
}