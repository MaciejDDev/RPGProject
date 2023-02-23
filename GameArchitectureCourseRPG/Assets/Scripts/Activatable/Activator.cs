using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] ActivatorMode _mode;

    [SerializeField] string _activatableTag;
    [DrawIf(nameof(_mode), ActivatorMode.AllInRadius)]
    [SerializeField] float _radius = 10f;


    public void Activate()
    {

        var activatables = FindObjectsOfType<Activatable>().
            Where(t => t.CompareTag(_activatableTag));

        switch (_mode)
        {
            case ActivatorMode.Nearest:
                activatables = activatables.
                OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).Take(1);
                break;
            case ActivatorMode.All: break;
            case ActivatorMode.AllInRadius:
                activatables = activatables.
                Where(t => Vector3.Distance(transform.position, t.transform.position) <= _radius);
                break;
            default:
                break;
        }


        foreach (var activatable in activatables)
            activatable.Toggle();
    }
}

public enum ActivatorMode{ Nearest, All, AllInRadius }
