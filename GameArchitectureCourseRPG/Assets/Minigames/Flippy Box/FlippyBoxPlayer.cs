using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxPlayer : MonoBehaviour, IRestart
{

    [SerializeField] Vector2 _jumpVelocity => FlippyboxMinigame.Instance.CurrentSettings.JumpVelocity;
    [SerializeField] float _growTime => FlippyboxMinigame.Instance.CurrentSettings.GrowTime;
   [SerializeField] float _spinSpeed = 50f;

    Rigidbody2D _rigidbody;
    Vector3 _startingPosition;
    Quaternion _startingRotation;
    float _elapsed;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            _rigidbody.velocity = _jumpVelocity;

        transform.Rotate(0f, 0f, Time.deltaTime * _spinSpeed);
        _elapsed += Time.deltaTime;
        float size = Mathf.Lerp(1f, 2f, _elapsed / _growTime);
        transform.localScale = new Vector3(size, size, 1f);
    }
    public void Restart()
    {
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
        _elapsed = 0f;
        transform.localScale = Vector3.one;
        GetComponent<SpriteRenderer>().color = FlippyboxMinigame.Instance.CurrentSettings.PlayerColor;
    }
}
