using System;
using UnityEngine;


[RequireComponent (typeof(Interactable))]
public class InteractableStatCondition : MonoBehaviour, IMet
{
    [SerializeField] int _requiredStatValue;
    [SerializeField] Stat _requiredStat;
    
    
    Interactable _interactable;
    [SerializeField] bool _skillupOnInteractionCompleted = true;

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
