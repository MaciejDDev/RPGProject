using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public void Activate()
    {
        var allActivatables = FindObjectsOfType<Activatable>();
        foreach (var activatable in allActivatables)
        {
            activatable.Toggle();
        }
    }
}
