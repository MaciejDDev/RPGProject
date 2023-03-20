using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player ActivePlayer { get; private set; }

    [SerializeField] Transform _shoulders;
    [SerializeField] ThirdPersonMover _mover;
    [SerializeField] StatsManager _statsManager;
    [SerializeField] Inventory _inventory;

    public Transform Shoulders => _shoulders;

    public StatsManager StatsManager => _statsManager;

    public Inventory Inventory => _inventory;

    private void  OnValidate()
    {
        _mover = GetComponent<ThirdPersonMover>();
        _statsManager = GetComponent<StatsManager>();
        _inventory = GetComponent<Inventory>();
    }
    public void Bind(PlayerData playerData)
    {
        _statsManager.Bind(playerData.StatDatas);
        _inventory.Bind(playerData.SlotDatas);
    }

    public void ToggleActive(bool state)
    {
        _mover.enabled = state;
        if (state)
        {
            ActivePlayer = this;
            UIManager.Instance.Bind(this);
            
        }
    }
}
