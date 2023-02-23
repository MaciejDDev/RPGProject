using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static Interactable _currentInteractable;
    static List<InteractableData> _datas;

    public static bool Interacting => _currentInteractable != null && !_currentInteractable.WasFullyInteracted;

    public static float InteractionProgress => _currentInteractable?.InteractionProgress ?? 0f;


    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E)) 
            _currentInteractable = Interactable.InteractablesInRange.FirstOrDefault();

        if (Input.GetKey(KeyCode.E) && _currentInteractable != null)
            _currentInteractable.Interact();
        else
            _currentInteractable = null;
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
