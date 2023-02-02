using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour, IRestart
{
    [SerializeField] float _moveSpeed = 4f;
    
    Rigidbody2D _rigidbody;
    Vector3 _startingPosition;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.left * _moveSpeed * Time.deltaTime;
        _rigidbody.position += movement;
        if(_rigidbody.position.x < -15f)
        {
            _rigidbody.position += new Vector2(30f, 0f);
        }
    }
    public void Restart()
    {
        transform.position = _startingPosition;
    }
}
