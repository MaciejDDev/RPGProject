using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    static Inspectable _currentInspectable;
    static List<InspectableData> _datas;

    public static bool Inspecting => _currentInspectable != null && !_currentInspectable.WasFullyInspected;

    public static float InspectionProgress => _currentInspectable?.InspectionProgress ?? 0f;


    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E)) 
            _currentInspectable = Inspectable.InspectablesInRange.FirstOrDefault();

        if (Input.GetKey(KeyCode.E) && _currentInspectable != null)
            _currentInspectable.Inspect();
        else
            _currentInspectable = null;
    }
    public static void Bind(List<InspectableData> datas)
    {
        _datas = datas;
        var allInspectables = GameObject.FindObjectsOfType<Inspectable>(true);
        foreach (var inspectable in allInspectables)
        {
            Bind(inspectable);
        }
    }

    public static void Bind(Inspectable inspectable)
    {
        var data = _datas.FirstOrDefault(t => t.Name == inspectable.name);
        if (data == null)
        {
            data = new InspectableData(); { data.Name = inspectable.name; }
            _datas.Add(data);
        }
        inspectable.Bind(data);
    }
}
