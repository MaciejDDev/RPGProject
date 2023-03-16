using System;
using UnityEngine;


[RequireComponent (typeof(Interactable))]
public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int _requiredStatValue;
    [SerializeField] StatType _requiredStat;
    [SerializeField] bool _skillupOnInteractionCompleted = true;
    
    
    Interactable _interactable;

    public string NotMetMessage { get; private set; }

    public string MetMessage { get; private set; }

    void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.InteractionCompleted += HandleInteractionCompleted;
        NotMetMessage = $"<color=red>{_requiredStat.name} ({_requiredStatValue})</color>";
        MetMessage = $"<color=green>{_requiredStat.name} ({_requiredStatValue})</color>";
    }

    void OnDestroy() => _interactable.InteractionCompleted -= HandleInteractionCompleted;

    void HandleInteractionCompleted()
    {
        if (_skillupOnInteractionCompleted)
            StatsManager.Instance.Modify(_requiredStat, 1);
    }

    public bool Met()
    {
        var statValue = StatsManager.Instance.GetStatValue(_requiredStat);
        return statValue >= _requiredStatValue;
    }
}
