using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Flag")]
public abstract class GameFlag : ScriptableObject
{
    public GameFlagData GameFlagData { get; private set; }

    public event Action Changed;
    protected void SendChanged() => Changed?.Invoke();

    public void Bind(GameFlagData gameFlagData)
    {
        GameFlagData = gameFlagData;
        SetFromData(GameFlagData.Value);
    }

    protected abstract void SetFromData(string value);
}


public class GameFlag<T> : GameFlag
{

    public T Value { get; private set; }

    void OnDisable() => Value = default;

    void OnEnable() => Value = default;
    public void Set(T value)
    {
        Value = value;
        GameFlagData.Value = Value.ToString();
        SendChanged();

    }

    protected override void SetFromData(string value)
    {
       
    }
}


[Serializable]
public class GameFlagData
{
    public string Name;
    public string Value;
}