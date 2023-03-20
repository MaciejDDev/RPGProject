using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player ActivePlayer { get; private set; }

    [SerializeField] Transform _shoulders;
    [SerializeField] ThirdPersonMover _mover;
    StatsManager _statsManager;

    public Transform Shoulders => _shoulders;

    public StatsManager StatsManager => _statsManager;



    private void  OnValidate()
    {
        _mover = GetComponent<ThirdPersonMover>();
        _statsManager = GetComponent<StatsManager>();
    }
    public void Bind(PlayerData playerData)
    {
        GetComponent<StatsManager>().Bind(playerData.StatDatas);
    }

    public void ToggleActive(bool state)
    {
        _mover.enabled = state;
        if (state)
            ActivePlayer = this;
    }
}
