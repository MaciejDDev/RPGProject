using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{

    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _value;
    
    
    StatData _statData;
    ToggleablePanel _toggleablePanel;

    public void Bind(StatData statData)
    {
        _statData = statData;
        _name.SetText(_statData.Name);
    }

    void Awake() => _toggleablePanel = GetComponentInParent<ToggleablePanel>();

    void Update()
    {
        if (_toggleablePanel.IsVisible)
        _value.SetText(_statData.Value.ToString());
    }
}
