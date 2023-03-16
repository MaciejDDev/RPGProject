using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{

    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _value;
    
    
    Stat _stat;
    ToggleablePanel _toggleablePanel;

    public void Bind(Stat stat)
    {
        _stat = stat;
        _name.SetText(_stat.Name);
    }

    void Awake() => _toggleablePanel = GetComponentInParent<ToggleablePanel>();

    void Update()
    {
        if (_toggleablePanel.IsVisible)
        {
            string format = "N" + _stat.StatType.AllowedDecimals;
            _value.SetText(_stat.GetValue().ToString(format));
        }
    }
}
