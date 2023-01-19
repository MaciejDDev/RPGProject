using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] List<GameFlag> _allFlags;

    public static FlagManager Instance { get; private set; }
    void Awake() => Instance = this;
    
    public void Set(string flagName, string value)
    {
        var flag = _allFlags.FirstOrDefault(t => t.name == flagName); 
        if (flag == null) 
        {
            Debug.LogError($"Flag not found {flagName}");
            return;
        }
        if(flag is IntGameFlag intGameFlag)
        {
            if (int.TryParse(value, out var intFlagValue))
                intGameFlag.Set(intFlagValue);
        }
    }
}