using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string _displayName;
    //para que no se pierda lo que había guardado antes de cambiar el nombre de vble.
    //[FormerlySerializedAs(oldName:"_name")] [SerializeField] string _displayName;
    [SerializeField] string _description;
    [SerializeField] Sprite _sprite;
    
    [Tooltip("Designer/programmer notes, not visible to player.")]
    [SerializeField] string _notes;

    public List<Step> Steps;
    

    public string Description => _description;

    public string DisplayName => _displayName;

    public Sprite Sprite => _sprite;
}


[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public string Instructions => _instructions;
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
    public override string ToString() => _objectiveType.ToString();
 
}
