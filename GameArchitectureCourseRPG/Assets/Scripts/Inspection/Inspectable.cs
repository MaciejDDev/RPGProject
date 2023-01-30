﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    [SerializeField] float _timeToInspect = 3f;
    [SerializeField] UnityEvent OnInspectionCompleted;
    [SerializeField] Inspectable _required;

    static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();
    
    
    InspectableData _data;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;
    public static event Action<bool> InspectablesInRangeChanged;

    public float InspectionProgress => _data.TimeInspected / _timeToInspect;

    public bool WasFullyInspected => InspectionProgress >= 1f;

    public bool MeetsConditions => _required == null || _required.WasFullyInspected;

    public void Bind(InspectableData inspectableData)
    {
        _data = inspectableData;
        if (_data.TimeInspected >= _timeToInspect)
            CompleteInspection();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !WasFullyInspected && MeetsConditions)
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
        if(_data.TimeInspected >= _timeToInspect)
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();

    }
}