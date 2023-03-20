using UnityEngine;

public class EquipmentPanel : ToggleablePanel
{
    Camera _portraitCamera;

    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        foreach (InventoryPanelSlot panelSlot in panelSlots)
        {
            panelSlot.Bind(inventory.GetEquipmentSlot(panelSlot.EquipmentSlotType));
        }

        if (_portraitCamera)
            _portraitCamera.enabled = false;

        _portraitCamera = inventory.GetComponentInChildren<Camera>();
        
        if (_portraitCamera)
            _portraitCamera.enabled = true;


    }
}
