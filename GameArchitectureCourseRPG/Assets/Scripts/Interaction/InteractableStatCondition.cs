using UnityEngine;

public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int _requiredStatValue;
    [SerializeField] Stat _requiredStat;
    public bool Met()
    {
        int statValue = StatsManager.Instance.GetStatValue(_requiredStat);
        return statValue >= _requiredStatValue;
    }
}
