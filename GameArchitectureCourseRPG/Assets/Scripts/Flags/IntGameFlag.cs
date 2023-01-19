using Ink.Runtime;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Counted Int Game Flag")]
public class IntGameFlag : GameFlag<int>
{
 
    public void Modify(int value)
    {
        Value += value;
        SendChanged();
    }

    internal void Set(int value)
    {
        Value = value;
        SendChanged();
    }
}

