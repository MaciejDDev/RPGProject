using System.Linq;
using UnityEngine;

public class InRangeOfDoorValidator : MonoBehaviour, IValidatePlacement
{

    [SerializeField] float _range = 10f;

    public bool IsValid()
    {
        if(GameObject.FindGameObjectsWithTag("Door").Any(t => Vector3.Distance(transform.position, t.transform.position) < _range))
            return true;
        
        return false;
    }
}
