using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemInteraction : MonoBehaviour
{

    public void GiveItem(Item item) => Player.ActivePlayer.Inventory.AddItem(item);


}
