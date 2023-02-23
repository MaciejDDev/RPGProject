using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static Interactable _currentInteractable;
    static List<InteractableData> _datas;

    public static bool Interacting { get; private set; }

    public static float InteractionProgress => _currentInteractable?.InteractionProgress ?? 0f;

    void Awake() => Interactable.InteractablesInRangeChanged += HandleInteractablesInRangeChanged;
    void OnDestroy() => Interactable.InteractablesInRangeChanged -= HandleInteractablesInRangeChanged;

    void HandleInteractablesInRangeChanged(bool obj)
    {
        var nearest = Interactable.InteractablesInRange.
            OrderBy(t => Vector3.Distance(t.transform.position, transform.position)).
            FirstOrDefault();
        _currentInteractable = nearest;
    }

    void Update()
    {

        if ( _currentInteractable != null && Input.GetKey(_currentInteractable.HotKey) )
        {
            _currentInteractable.Interact();
            Interacting = true;
        }
        else
            Interacting = false;

    }
    public static void Bind(List<InteractableData> datas)
    {
        _datas = datas;
        var allInteractables = GameObject.FindObjectsOfType<Interactable>(true);
        foreach (var interactable in allInteractables)
        {
            Bind(interactable);
        }
    }

    public static void Bind(Interactable interactable)
    {
        var data = _datas.FirstOrDefault(t => t.Name == interactable.name);
        if (data == null)
        {
            data = new InteractableData(); { data.Name = interactable.name; }
            _datas.Add(data);
        }
        interactable.Bind(data);
    }
}
