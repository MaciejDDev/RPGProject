﻿using UnityEngine;

public class MetBoolFlagCondition : MonoBehaviour, IMet
{

    [SerializeField] BoolGameFlag _requiredFlag;

    public string NotMetMessage => _requiredFlag.name;

    public bool Met() => _requiredFlag.Value;

}
