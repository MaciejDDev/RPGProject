using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{

    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _value;
    
    
    StatData _statData;

    public void Bind(StatData statData)
    {
        _statData = statData;
    }

    void Start()
    {
        
    }

    void Update()
    {
        _name.SetText(_statData.Name);
        _value.SetText(_statData.Value.ToString());
    }
}
