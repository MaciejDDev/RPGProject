using UnityEngine;

[CreateAssetMenu(menuName ="Stat Type")]
public class StatType : ScriptableObject
{
    public int AllowedDecimals = 0;
    public int DefaultValue = 1;
    public int MinimumValue = 0;
}
