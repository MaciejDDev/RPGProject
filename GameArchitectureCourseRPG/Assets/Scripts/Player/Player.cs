using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _shoulders;

    public Transform Shoulders => _shoulders;

    internal void Bind(PlayerData playerData)
    {
        GetComponent<StatsManager>().Bind(playerData.StatDatas);
    }
}
