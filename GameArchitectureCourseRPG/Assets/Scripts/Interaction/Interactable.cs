using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{
    [SerializeField] float _timeToInteract = 3f;
    [FormerlySerializedAs("_completedInspectionText")][SerializeField, TextArea] string _completedInteractionText;
    [SerializeField] UnityEvent OnInteractionCompleted;
    [SerializeField] bool _requireMinigame;
    [SerializeField] MinigameSettings _minigameSettings;

    static HashSet<Interactable> _interactablesInRange = new HashSet<Interactable>();
    
    
    InteractableData _data;
    IMet[] _allConditions;

    public static IReadOnlyCollection<Interactable> InteractablesInRange => _interactablesInRange;
    public static event Action<bool> InteractablesInRangeChanged;
    public static event Action<Interactable, string> AnyInteractionComplete;
    public float InteractionProgress => _data?.TimeInteracted ?? 0f / _timeToInteract;

    public bool WasFullyInteracted => InteractionProgress >= 1f;


    void Awake() => _allConditions = GetComponents<IMet>();

    IEnumerator Start()
    {
        yield return null;
        if (_data == null)
            InteractionManager.Bind(this);
    }

    public bool MeetsConditions()
    {
        
        foreach (var condition in _allConditions)
        {
            if (condition.Met() == false)
                return false;
        }
        return true;
    }

    public void Bind(InteractableData interactableData)
    {
        _data = interactableData;
        if (WasFullyInteracted)
            RestoreInteractionState();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !WasFullyInteracted && MeetsConditions())
        {
            _interactablesInRange.Add(this);
            InteractablesInRangeChanged?.Invoke(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_interactablesInRange.Remove(this))
                InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());

        }
    }
    public void Interact()
    {
        if(WasFullyInteracted)
            return; 
        _data.TimeInteracted += Time.deltaTime;
        if(WasFullyInteracted)
        {
            if (_requireMinigame)
            {
                _interactablesInRange.Remove(this);
                InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());
                MinigameManager.Instance.StartMinigame(_minigameSettings, HandleMinigameCompleted);
            }
            else
                CompleteInteraction();
        }
    }

    void HandleMinigameCompleted(MinigameResult result)
    {
        if (result == MinigameResult.Won)
            CompleteInteraction();
        else if (result == MinigameResult.Lost)
        {
            _interactablesInRange.Add(this);
            InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());
            _data.TimeInteracted = 0f;
        }
    }
void CompleteInteraction()
    {
        
        _interactablesInRange.Remove(this);
        InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());
        OnInteractionCompleted?.Invoke();
        AnyInteractionComplete?.Invoke(this, _completedInteractionText);

    }

    void RestoreInteractionState()
    {
        OnInteractionCompleted?.Invoke();
    }
}
