using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected InteractionType _interactionType;

    [SerializeField] float _timeToInteract = 3f;
    [SerializeField] UnityEvent OnInteractionCompleted;
    [SerializeField] bool _requireMinigame;
    [SerializeField] MinigameSettings _minigameSettings;

    static HashSet<Interactable> _interactablesInRange = new HashSet<Interactable>();
    
    
    protected InteractableData _data;
    IMet[] _allConditions;

    public virtual InteractionType InteractionType => _interactionType;

    //public KeyCode HotKey => _interactionType.HotKey;

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

    void OnValidate()
    {
        if (_interactionType == null)
            _interactionType = Resources.FindObjectsOfTypeAll<InteractionType>().
                Where(t => t.IsDefault).FirstOrDefault();
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
     protected virtual void CompleteInteraction()
    {

        _interactablesInRange.Remove(this);
        SendInteractionComplete();

    }

    protected void SendInteractionComplete()
    {
        InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());
        OnInteractionCompleted?.Invoke();
        AnyInteractionComplete?.Invoke(this, InteractionType.CompletedInteraction);
    }

    void RestoreInteractionState()
    {
        OnInteractionCompleted?.Invoke();
    }
}
