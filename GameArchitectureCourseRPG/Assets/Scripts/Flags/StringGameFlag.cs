using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Flag/String")]
public class StringGameFlag : GameFlag<string>
{
    protected override void SetFromData(string value)
    {
        Set(value);
    }

}