using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Game Flag/Bool")]
public class BoolGameFlag : GameFlag<bool>
{
    protected override void SetFromData(string value)
    {
        if (bool.TryParse(value, out var boolValue))
            Set(boolValue);
    }

}

