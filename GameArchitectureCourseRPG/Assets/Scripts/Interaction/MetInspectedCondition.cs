using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MetInspectedCondition : MonoBehaviour, IMet
{

    [SerializeField] Interactable _requiredInspectable;

    public string NotMetMessage { get; }

    public bool Met() => _requiredInspectable.WasFullyInteracted;

    void OnDrawGizmos()
    {
        if(_requiredInspectable != null)
        {
            Gizmos.color = Met() ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, _requiredInspectable.transform.position);
        }
    }
}
