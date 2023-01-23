using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] GameFlag[] _allFlags;
    Dictionary<string, GameFlag> _flagsByName;

    public static FlagManager Instance { get; private set; }
    void Awake() => Instance = this;
    void Start() => _flagsByName = _allFlags.ToDictionary(k => k.name, v => v);
    void OnValidate() => _allFlags = GetAllInstances<GameFlag>();

    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }

    public void Set(string flagName, string value)
    {
        if(_flagsByName.TryGetValue(flagName, out var flag) == false) 
        {
            Debug.LogError($"Flag not found {flagName}");
            return;
        }
        if(flag is IntGameFlag intGameFlag)
        {
            if (int.TryParse(value, out var intFlagValue))
                intGameFlag.Set(intFlagValue);
        }
        else if (flag is BoolGameFlag boolGameFlag)
        {
            if (bool.TryParse(value, out var boolFlagValue))
                boolGameFlag.Set(boolFlagValue);
        }
        else if (flag is StringGameFlag stringGameFlag)
        {
            
                stringGameFlag.Set(value);
        }
        else if (flag is DecimalGameFlag decimalGameFlag)
        {
            if (decimal.TryParse(value, out var decimalFlagValue))
                decimalGameFlag.Set(decimalFlagValue);
        }
    }
}