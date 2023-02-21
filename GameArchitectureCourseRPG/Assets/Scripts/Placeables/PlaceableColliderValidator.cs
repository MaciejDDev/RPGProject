using UnityEngine;

public class PlaceableColliderValidator : MonoBehaviour, IValidatePlacement
{
    BoxCollider _collider;
    Collider[] _results = new Collider[100];

    public Collider CurrentlyBlockedBy;

    void Awake() => _collider = GetComponent<BoxCollider>();

    public bool IsValid()
    {
        int hits = Physics.OverlapBoxNonAlloc(_collider.transform.position + _collider.center,
            _collider.bounds.extents, 
            _results,
            _collider.transform.rotation);

        CurrentlyBlockedBy = null;

        for (int i = 0; i < hits; i++)
        {
            if (_results[i].transform.IsChildOf(transform))
                continue;

            CurrentlyBlockedBy = _results[i];

            return false;
        }
        return true;
    
    }
}
