using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Vector2 _direction = Vector2.right;
    [SerializeField] float _distance = 5f;
    [SerializeField] float _speed = 2f;
    private Vector3 _startPosition;
    private float _progress;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        _progress += _speed * Time.deltaTime;
        float movementFactor = Mathf.PingPong(_progress, _distance) / _distance;
        transform.position = _startPosition + (Vector3)(_direction.normalized * _distance * movementFactor);
    }
}


