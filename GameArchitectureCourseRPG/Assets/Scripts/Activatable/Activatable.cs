using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activatable : MonoBehaviour
{

    [SerializeField] UnityEvent _onActivated;
    [SerializeField] UnityEvent _onDeactivated;
    
    
    bool _activated;


    [ContextMenu("Toggle")]
    public void Toggle()
    {
        _activated = !_activated;
        if (_activated)
            _onActivated.Invoke();
        else
            _onDeactivated.Invoke();
    }

}
