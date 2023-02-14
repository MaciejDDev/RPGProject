using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] InventoryPanelSlot _overflowSlot;

    IEnumerator Start()
    {
        yield return null;
        Bind(Inventory.Instance);
    }

    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>().Where(t => t != _overflowSlot).ToArray();
        for (int i = 0; i < panelSlots.Length; i++)
        {
            panelSlots[i].Bind(inventory.GeneralSlots[i]);
        }
        _overflowSlot.Bind(inventory.TopOverflowSlot);
    }
}
