using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [SerializeField] Vector3 _magnitude = Vector3.down;

    Vector3 _startingPosition;
    Vector3 _endingPosition;
    float _elapsed;

    void OnEnable()
    {
        _elapsed = 0f;
        _endingPosition = _startingPosition + _magnitude;
    }

    void OnDisable() => transform.position = _startingPosition;
    void Awake() => _startingPosition = transform.position;

    void Update()
    {
        _elapsed += Time.deltaTime;
        transform.position = Vector3.Lerp(_startingPosition, _endingPosition, _elapsed);
        
    }
}
