using Ink.Runtime;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag/Counted Int")]
public class IntGameFlag : GameFlag<int>
{
 
    public void Modify(int value)
    {
        Value += value;
        SendChanged();
    }

    
}

