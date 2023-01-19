using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag")]
public abstract class GameFlag : ScriptableObject
{
    public event Action Changed;
    protected void SendChanged() => Changed?.Invoke();
}

[CreateAssetMenu(menuName = "Game Flag")]
public class GameFlag<T> : GameFlag
{

    public T Value { get; protected set; }

    void OnDisable() => Value = default;

    void OnEnable() => Value = default;
    
}