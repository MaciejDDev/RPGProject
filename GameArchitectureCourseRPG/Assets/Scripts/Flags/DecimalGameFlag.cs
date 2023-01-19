using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Decimal Game Flag")]
public class DecimalGameFlag : GameFlag<decimal>
{
    public void Set(decimal value)
    {
        Value = value;
        SendChanged();

    }

}