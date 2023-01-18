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
    
    int _currentStepIndex;

    public event Action Progressed;
    
    public List<Step> Steps;

    public string Description => _description;

    public string DisplayName => _displayName;

    public Sprite Sprite => _sprite;

    public Step CurrentStep => Steps[_currentStepIndex];

    private void OnEnable() => _currentStepIndex = 0;
    public void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if(currentStep.HasAllObjectivesCompleted())
        {
            _currentStepIndex++;
            Progressed?.Invoke();
            //do whatever we do when a quest progresses like update the UI
        }
    }
    
    private Step GetCurrentStep() => Steps[_currentStepIndex];
}


[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public string Instructions => _instructions;
    public List<Objective> Objectives;

    public bool HasAllObjectivesCompleted()
    {
        return Objectives.TrueForAll(t => t.IsCompleted);
    }
}

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType _objectiveType;
    [SerializeField] GameFlag _gameFlag;

    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }
    public bool IsCompleted 
    {
        get
        {
            switch(_objectiveType)
            {
                case ObjectiveType.Flag: return _gameFlag.Value;
                default: return false;

            }
        }
            
    }
    public override string ToString()
    {
        switch(_objectiveType)
        {
            case ObjectiveType.Flag: return _gameFlag.name;
            default: return _objectiveType.ToString();

        }
    }
 
}
