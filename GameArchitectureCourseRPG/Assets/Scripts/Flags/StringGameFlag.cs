using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "String Game Flag")]
public class StringGameFlag : GameFlag<string>
{
    public void Set(string value)
    {
        Value = value;
        SendChanged();

    }

}