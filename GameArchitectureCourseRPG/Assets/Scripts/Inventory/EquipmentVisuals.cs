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
        //yield return null;
        foreach (var visual in Visuals)
            visual.DefaultMaterial = visual.SkinnedMeshRenderers.FirstOrDefault()?.material;
        
        foreach (var slot in Inventory.Instance.EquipmentSlots)
        {
            slot.Changed += (Item added, Item removed) => UpdateEquipmentVisual(slot);
            UpdateEquipmentVisual(slot);
        }
    }

    void UpdateEquipmentVisual(ItemSlot slot)
    {
        foreach (var visual in Visuals.Where(t => t.EquipmentSlotType == slot.EquipmentSlotType))
        {
            foreach (var skinnedMeshRenderer in visual.SkinnedMeshRenderers)
                skinnedMeshRenderer.material = slot.Item?.EquipMaterial ?? visual.DefaultMaterial;
            

                //Debug.Log($"Visual Root: { visual.VisualModelRoot} ");
                if (visual.VisualModelRoot != null)
                {
                    for (int i = 0; i < visual.VisualModelRoot.childCount; i++)
                    {
                        var model = visual.VisualModelRoot.GetChild(i);
                        model.gameObject.SetActive(model.name == slot.Item?.ModelName);
                        //Debug.LogError("Setting active/inactive weapons");
                    }
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
    public Transform VisualModelRoot;
}