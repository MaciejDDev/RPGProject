using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/Decimal")]
public class DecimalGameFlag : GameFlag<decimal>
{
    protected override void SetFromData(string value)
    {
        if (decimal.TryParse(value, out var decimalValue))
            Set(decimalValue);
    }

}