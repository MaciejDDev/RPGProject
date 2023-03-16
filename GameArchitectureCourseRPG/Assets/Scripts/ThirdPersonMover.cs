using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 1000f;
    [SerializeField] StatType _moveSpeed;
    Rigidbody _rigidbody;
    Animator _animator;
    float _mouseMovement;
    [SerializeField] StatType _energy;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Update() => _mouseMovement += Input.GetAxis("MouseX");
    void FixedUpdate() 
    {

        //if (ToggleablePanel.AnyVisible)
        //{
        //    _animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
        //    _animator.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
        //    return;
        //}
        transform.Rotate(0, _mouseMovement * Time.deltaTime * _turnSpeed, 0);
        _mouseMovement = 0;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");    
        if (Input.GetKey(KeyCode.LeftShift))
            vertical *= 2f;
        

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= StatsManager.Instance.GetStatValue(_moveSpeed) * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;

        StatsManager.Instance.Modify(_energy, - offset.magnitude);
     
        _rigidbody.MovePosition(transform.position + offset);
        if (_animator != null)
        {
            _animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        }
    }
}
