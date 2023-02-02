using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float _moveSpeed = 4f;

    void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
    void FixedUpdate()
    {
        Vector2 movement = Vector2.left * _moveSpeed * Time.deltaTime;
        _rigidbody.position += movement;
        if(_rigidbody.position.x < -15f)
        {
            _rigidbody.position += new Vector2(30f, 0f);
        }
    }
}
