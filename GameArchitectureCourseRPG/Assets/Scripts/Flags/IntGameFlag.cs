using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Counted Int Game Flag")]
public class IntGameFlag : ScriptableObject
{

    public event Action Changed;

    public int Value { get; private set; }

    void OnEnable() => Value = default;

    void OnDisable() => Value = default;

    public void Modify(int value)
    {
        Value += value;
        Changed?.Invoke();
    }

}

