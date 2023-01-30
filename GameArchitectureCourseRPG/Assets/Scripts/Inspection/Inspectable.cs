using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    [SerializeField] float _timeToInspect = 3f;
    [SerializeField] UnityEvent OnInspectionCompleted;
    static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();
    float _timeInspected;

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;
    public static event Action<bool> InspectablesInRangeChanged;

    public float InspectionProgress => _timeInspected / _timeToInspect;

    public bool WasFullyInspected { get; private set; }



    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !WasFullyInspected)
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
        _timeInspected += Time.deltaTime;
        if(_timeInspected >= _timeToInspect)
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        WasFullyInspected = true;
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();

    }
}