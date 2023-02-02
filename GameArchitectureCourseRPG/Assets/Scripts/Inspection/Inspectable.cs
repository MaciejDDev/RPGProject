using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    [SerializeField] float _timeToInspect = 3f;
    [SerializeField, TextArea] string _completedInspectionText;
    [SerializeField] UnityEvent OnInspectionCompleted;
    [SerializeField] bool _requireMinigame;

    static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();
    
    
    InspectableData _data;
    IMet[] _allConditions;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;
    public static event Action<bool> InspectablesInRangeChanged;
    public static event Action<Inspectable, string> AnyInspectionComplete;
    public float InspectionProgress => _data?.TimeInspected ?? 0f / _timeToInspect;

    public bool WasFullyInspected => InspectionProgress >= 1f;


    void Awake() => _allConditions = GetComponents<IMet>();
    public bool MeetsConditions()
    {
        
        foreach (var condition in _allConditions)
        {
            if (condition.Met() == false)
                return false;
        }
        return true;
    }

    public void Bind(InspectableData inspectableData)
    {
        _data = inspectableData;
        if (WasFullyInspected)
            RestoreInspectionState();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !WasFullyInspected && MeetsConditions())
        {
            _inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_inspectablesInRange.Remove(this))
                InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());

        }
    }
    public void Inspect()
    {
        if(WasFullyInspected)
            return; 
        _data.TimeInspected += Time.deltaTime;
        if(WasFullyInspected)
        {
            if (_requireMinigame)
                MinigameManager.Instance.StartMinigame(CompleteInspection);
            else
                CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
        AnyInspectionComplete?.Invoke(this, _completedInspectionText);

    }

    void RestoreInspectionState()
    {
        OnInspectionCompleted?.Invoke();
    }
}
