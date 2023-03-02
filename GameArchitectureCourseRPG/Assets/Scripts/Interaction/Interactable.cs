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
    [SerializeField] UnityEvent OnLastInteractionCompleted;
    [SerializeField] bool _rerunLastInteractionCompletedOnGameLoad;
    [SerializeField] bool _requireMinigame;
    [SerializeField] MinigameSettings _minigameSettings;
    [SerializeField] int _maxInteractions = 1;
    static HashSet<Interactable> _interactablesInRange = new HashSet<Interactable>();
    
    
    protected InteractableData _data;
    IMet[] _allConditions;

    public virtual InteractionType InteractionType => _interactionType;

    //public KeyCode HotKey => _interactionType.HotKey;

    public static IReadOnlyCollection<Interactable> InteractablesInRange => _interactablesInRange;
    public static event Action<bool> InteractablesInRangeChanged;
    public static event Action<Interactable, string> AnyInteractionComplete;
   
    public event Action InteractionCompleted;
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
        OnBound();
    }

    protected virtual void OnBound()
    {
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
     void CompleteInteraction()
    {
        _data.InteractionCount++;

        if (_maxInteractions == 0)
        {
            _data.TimeInteracted = 0f;
        }
        else
        {
            if (_data.InteractionCount < _maxInteractions)
                _data.TimeInteracted = 0f;
            else
                OnLastInteractionCompleted.Invoke();
        }

        InteractionCompleted?.Invoke();

        if (WasFullyInteracted)
            _interactablesInRange.Remove(this);

        SendInteractionComplete();
    }


    protected void SendInteractionComplete()
    {
        InteractablesInRangeChanged?.Invoke(_interactablesInRange.Any());
        OnInteractionCompleted?.Invoke();
        AnyInteractionComplete?.Invoke(this, InteractionType.CompletedInteraction);
    }

    protected void RestoreInteractionState()
    {
        if (_rerunLastInteractionCompletedOnGameLoad)
        OnLastInteractionCompleted?.Invoke();
    }
}
