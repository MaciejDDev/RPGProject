using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName ="Bool Game Flag")]
public class GameFlag : ScriptableObject
{
    public bool Value;

    void OnEnable() => Value = default;
}
