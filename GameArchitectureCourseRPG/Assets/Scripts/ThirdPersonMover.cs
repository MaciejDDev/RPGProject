using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 1000f;
    [SerializeField] float _moveSpeed = 5f;
    Rigidbody _rigidbody;
    Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        var mouseMovement = Input.GetAxis("MouseX");
        transform.Rotate(0, mouseMovement * Time.deltaTime * _turnSpeed, 0);
    }

    void FixedUpdate() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");    
        if (Input.GetKey(KeyCode.LeftShift))
            vertical *= 2f;
        

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;
        _rigidbody.MovePosition(transform.position + offset);
        
        _animator.SetFloat("Speed", vertical, 0.1f, Time.deltaTime);

    }
}
