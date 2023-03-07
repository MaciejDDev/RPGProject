using System;
using UnityEngine;


[RequireComponent (typeof(Interactable))]
public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int _requiredStatValue;
    [SerializeField] Stat _requiredStat;
    [SerializeField] bool _skillupOnInteractionCompleted = true;
    
    
    Interactable _interactable;

    public string NotMetMessage => $"<color=red>{_requiredStat.name} ({_requiredStatValue})</color>";

    public string MetMessage => $"<color=green>{_requiredStat.name} ({_requiredStatValue})</color>";

    void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.InteractionCompleted += HandleInteractionCompleted;
    }

    void OnDestroy() => _interactable.InteractionCompleted -= HandleInteractionCompleted;

    void HandleInteractionCompleted()
    {
        if (_skillupOnInteractionCompleted)
            StatsManager.Instance.Modify(_requiredStat, 1);
    }

    public bool Met()
    {
        int statValue = StatsManager.Instance.GetStatValue(_requiredStat);
        return statValue >= _requiredStatValue;
    }
}
