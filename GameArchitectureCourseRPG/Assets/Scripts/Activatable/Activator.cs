using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] float _radius = 10f;
    [SerializeField] string _activatableTag;

    public void Activate()
    {

        var allActivatablesMatchingTag = FindObjectsOfType<Activatable>().
            Where(t => t.CompareTag(_activatableTag));


        var allActivatablesInRange = allActivatablesMatchingTag.
            Where(t => Vector3.Distance(transform.position, t.transform.position) <= _radius);
        foreach (var activatable in allActivatablesInRange)
        {
            activatable.Toggle();
        }
    }
}
