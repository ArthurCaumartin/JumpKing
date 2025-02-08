using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class JumpControler : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [Header("Jump :")]
    [SerializeField] private float _jumpForce;
    [SerializeField, Range(0, 1)] private float _xJumpForce;
    [SerializeField, Range(0, 1)] private float _yJumpForce;
    [SerializeField] private float _jumpChargeSpeed;
    [SerializeField] private float _maxJumpCharge;
    [SerializeField] private float _jumpCharge;
    [Space]
    [SerializeField, Range(0, 1)] private float _velocityKeepFactorOnHit;
    private Vector2 _moveAxis;
    private Rigidbody2D _rigidbody;
    private float _jumpInputValue;
    private Vector2 _lookDirection;
    private Vector2 _lastFrameVelocity;
    private bool _canMove = true;
    private GroundCheck _groundCheck;

    void Start()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lookDirection.x = 1;
    }

    void Update()
    {
        if (_groundCheck.IsGrounded() && _rigidbody.velocity.y == 0 && _jumpInputValue == 0)
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
        }

        if (_jumpInputValue > .5f && _jumpCharge < _maxJumpCharge)
        {
            _jumpCharge += Time.deltaTime * _jumpChargeSpeed;
        }
    }

    void FixedUpdate()
    {
        if (_moveAxis != Vector2.zero)
            MovePalyer();

        _lastFrameVelocity = _rigidbody.velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // print(other.contacts[0].normal);
        if (other.contacts[0].normal == Vector2.left || other.contacts[0].normal == Vector2.right)
        {
            _rigidbody.velocity = new Vector2(-_lastFrameVelocity.x * _velocityKeepFactorOnHit, _lastFrameVelocity.y);
        }

        if (other.contacts[0].normal == Vector2.up)
            _rigidbody.velocity = Vector2.zero;
    }

    void MovePalyer()
    {
        if (!_canMove)
            return;

        Vector2 newVelocity = new Vector2(_moveAxis.x * _moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = newVelocity;
    }

    void Jump()
    {
        if (_jumpCharge > 1)
        {
            Vector2 jumpDirection = ComputeJumpDirection();
            _rigidbody.AddForce(jumpDirection * _jumpForce * _jumpCharge, ForceMode2D.Impulse);
        }

        _jumpCharge = 0;
    }

    Vector2 ComputeJumpDirection()
    {
        Vector2 jumpDirection;

        float chargeFactor = Mathf.InverseLerp(0, _maxJumpCharge, _jumpCharge);
        jumpDirection.y = chargeFactor;
        jumpDirection.x = 1 - chargeFactor;
        // (1 - ratio) = l'inverse du ratio

        //! Clamp en x pour pas aller full en haut
        if (jumpDirection.x < .1f)
            jumpDirection.x = .1f;

        jumpDirection.x *= _lookDirection.x;
        jumpDirection = jumpDirection.normalized;

        jumpDirection.x *= _xJumpForce;
        jumpDirection.y *= _yJumpForce;

        return jumpDirection;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, ComputeJumpDirection().normalized);
    }

    public void OnMove(InputValue value)
    {
        _moveAxis = value.Get<Vector2>();
        if (_moveAxis.x != 0)
            _lookDirection.x = _moveAxis.x;
    }

    public void OnJumpCharge(InputValue value)
    {
        float floatValue = value.Get<float>();
        _jumpInputValue = floatValue;

        if (floatValue < .5f)
        {
            Jump();
        }
        // print(floatValue);
    }
}
