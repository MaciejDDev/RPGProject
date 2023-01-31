using System;
using UnityEngine;

public class MetInspectedCondition : MonoBehaviour
{

    [SerializeField] Inspectable _requiredInspectable;


    public bool Met() => _requiredInspectable.WasFullyInspected;
}