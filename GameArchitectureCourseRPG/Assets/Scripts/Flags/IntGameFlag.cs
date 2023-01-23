using Ink.Runtime;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/Counted Int")]
public class IntGameFlag : GameFlag<int>
{

    public void Modify(int value) => Set(Value + value);

    protected override void SetFromData(string value)
    {
        if (int.TryParse(value, out var intValue))
            Set(intValue);
    }
}

