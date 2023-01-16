using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _description;

    [Tooltip("Designer/programmer notes, not visible to player.")]
    [SerializeField] string _notes;

    public List<Step> Steps;
}


[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public List<Objective> Objectives;
}

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType _objectiveType;
    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }
}
