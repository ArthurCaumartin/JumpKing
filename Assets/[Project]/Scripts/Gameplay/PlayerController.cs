using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 5f;
    public float _maxJumpForce = 10f;
    public float _maxChargeTime = 1f;
    private Rigidbody2D _rb;
    private float _chargeTime = 0f;
    private PlayerInput _playerInput;
    private InputAction _jumpAction;
    private Vector2 _moveAxis;
    private float _lastDirection = 0f;
    [SerializeField] GroundCheck groundChek;
    private Rigidbody2D _rigidbody;
    private Vector2 _lastFrameVelocity;
    [SerializeField] private AnimationCurve _xCurve;
    [SerializeField] private AnimationCurve _yCurve;
    [SerializeField] private float _xJumpForce;
    [SerializeField] private float _yJumpForce;
    [SerializeField, Range(0, 1)] private float _velocityKeepFactorOnHit;
    private Transform _currentPlatform;
    private Vector3 _lastPlatformPosition;
    private LineRenderer _lineRenderer;
    bool isPush = false;

    private TileReeder _tileReader;
    private Vector2 _iceVelocity;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody2D>();

        _playerInput = GetComponent<PlayerInput>();
        _jumpAction = _playerInput.actions["Jump"];

        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = 2;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.green;
        _lineRenderer.endColor = Color.green;
        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (_rigidbody.velocity.y <= 0) isPush = false;
        if (isPush == true) return;
        MoveHorizontal();
        GetLastDirection();
        HandlePlatformMovement();

        if (_jumpAction.IsPressed())
        {
            ChargeJump();
        }
        if (_jumpAction.WasReleasedThisFrame())
        {
            ReleaseJump();
        }

        _lastFrameVelocity = _rb.velocity;
        float chargePercentage = (_chargeTime / _maxChargeTime) * 100f;
        //ChargeTimeText.text = chargePercentage.ToString("F0") + "%";
    }

    public void ChargeJump()
    {
        if (groundChek.IsGrounded() && _rb.velocity.x == 0)
        {
            Debug.DrawRay(transform.position, new Vector2(_lastDirection * _xJumpForce * _xCurve.Evaluate(_chargeTime / _maxChargeTime), _yJumpForce * _yCurve.Evaluate(_chargeTime / _maxChargeTime)));
            _chargeTime += Time.deltaTime;
            _chargeTime = Mathf.Clamp(_chargeTime, 0f, _maxChargeTime);
        }
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, (Vector2)transform.position +
        new Vector2(_lastDirection * _xJumpForce * _xCurve.Evaluate(_chargeTime / _maxChargeTime), _yJumpForce * _yCurve.Evaluate(_chargeTime / _maxChargeTime)));
    }

    public void ReleaseJump()
    {
        if (groundChek.IsGrounded())
        {
            if (_chargeTime > 0.25)
            {
                float jumptime = _chargeTime / _maxChargeTime;
                float jumpForce = Mathf.Lerp(0f, _maxJumpForce, jumptime);
                _rb.AddForce(new Vector2(_lastDirection * _xJumpForce * _xCurve.Evaluate(jumptime), _yJumpForce * _yCurve.Evaluate(jumptime)) * jumpForce, ForceMode2D.Impulse);
                _chargeTime = 0;
                _lineRenderer.enabled = false;
            }
            else
            {
                _chargeTime = 0;
            }
        }
    }

    private void GetLastDirection()
    {
        if (_moveAxis.x != 0f)
        {
            _lastDirection = _moveAxis.x;
        }
        Debug.DrawRay(transform.position, new Vector2(_lastDirection, 0), Color.red);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            _currentPlatform = other.transform;
            _lastPlatformPosition = _currentPlatform.position;
        }
        if (other.contacts[0].normal == Vector2.left || other.contacts[0].normal == Vector2.right)
        {
            _rigidbody.velocity = new Vector2(-_lastFrameVelocity.x, _lastFrameVelocity.y) * _velocityKeepFactorOnHit;
        }

        if (other.contacts[0].normal == Vector2.up)
            _rigidbody.velocity = Vector2.zero;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            _currentPlatform = null;
        }
    }

    private void MoveHorizontal()
    {
        if (!groundChek.IsGrounded() || _rb.velocity.y != 0) return;

        if (_tileReader.isGroundIce())
        {
            Vector3 targetVel = new Vector2(_moveAxis.x * _moveSpeed, _rb.velocity.y);

            targetVel.x -= Time.deltaTime * Mathf.Sign(targetVel.x);

            _rb.velocity = new Vector2(_moveAxis.x * _moveSpeed, _rb.velocity.y);
            return;
        }

        _rb.velocity = new Vector2(_moveAxis.x * _moveSpeed, _rb.velocity.y);
    }

    private void HandlePlatformMovement()
    {
        if (_currentPlatform != null)
        {
            Vector3 platformMovement = _currentPlatform.position - _lastPlatformPosition;
            transform.position += platformMovement;
            _lastPlatformPosition = _currentPlatform.position;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _moveAxis = inputValue.Get<Vector2>();
    }

    public void Move(int Direction)
    {
        _moveAxis = new Vector2(Direction, 0);
    }
    public void PushPlayer(Vector2 direction, float force)
    {
        isPush = true;
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }
}