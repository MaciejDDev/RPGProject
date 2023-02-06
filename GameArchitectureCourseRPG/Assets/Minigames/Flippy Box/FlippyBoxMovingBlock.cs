using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour, IRestart
{
    
    Rigidbody2D _rigidbody;
    Vector3 _startingPosition;
    float MoveSpeed => FlippyboxMinigame.Instance.CurrentSettings.MovingBlockSpeed;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.left * MoveSpeed * Time.deltaTime;
        _rigidbody.position += movement;
        if(transform.localPosition.x < -15f)
        {
            _rigidbody.position += new Vector2(30f, 0f);
        }
    }
    public void Restart()
    {
        transform.position = _startingPosition;
    }
}
