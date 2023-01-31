using UnityEngine;

public class MetIntFlagCondition : MonoBehaviour, IMet
{

    [SerializeField] IntGameFlag _requiredFlag;
    [SerializeField] int _requiredValue;

    public bool Met() => _requiredFlag.Value >= _requiredValue;
}