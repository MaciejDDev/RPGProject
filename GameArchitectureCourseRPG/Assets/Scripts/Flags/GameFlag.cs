using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName ="Bool Game Flag")]
public class GameFlag : ScriptableObject
{
    public static event Action AnyChanged;

    public bool Value { get; private set; }

    void OnEnable() => Value = default;

    void OnDisable() => Value = default;

    public void Set(bool value)
    { 
        Value = value;
        AnyChanged?.Invoke();
    }

}
