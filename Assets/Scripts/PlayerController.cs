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
    [SerializeField]  private float _xJumpForce;
    [SerializeField]  private float _yJumpForce;
    [SerializeField, Range(0, 1)] private float _velocityKeepFactorOnHit;
    [SerializeField] TMP_Text ChargeTimeText;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody2D>();

        _playerInput = GetComponent<PlayerInput>();
        _jumpAction = _playerInput.actions["Jump"];
    }

    void Update()
    {
        MoveHorizontal();
        GetLastDirection();
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
        ChargeTimeText.text = chargePercentage.ToString("F0") + "%";
    }
    public void ChargeJump()
    {
        if (groundChek.IsGrounded() && _rb.velocity.x == 0)
        {
            _chargeTime += Time.deltaTime;
            _chargeTime = Mathf.Clamp(_chargeTime, 0f, _maxChargeTime);
        }
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
        // print(other.contacts[0].normal);
        if (other.contacts[0].normal == Vector2.left || other.contacts[0].normal == Vector2.right)
        {
            _rigidbody.velocity = new Vector2(-_lastFrameVelocity.x, _lastFrameVelocity.y) * _velocityKeepFactorOnHit;
        }

        if (other.contacts[0].normal == Vector2.up)
            _rigidbody.velocity = Vector2.zero;
        if (other.contacts[0].normal == Vector2.down)
        {
            float horizontalVelocity = _rigidbody.velocity.x;
            float verticalVelocity = Mathf.Min(0, _rigidbody.velocity.y * -0.2f);
            _rigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
    }

    private void MoveHorizontal()
    {
        if (!groundChek.IsGrounded() || _rb.velocity.y != 0) return;
        _rb.velocity = new Vector2(_moveAxis.x * _moveSpeed, _rb.velocity.y);
    }
    private void OnMove(InputValue inputValue)
    {
        _moveAxis = inputValue.Get<Vector2>();
    }
    public void Move(int Direction)
    {
        _moveAxis = new Vector2(Direction, 0);
    }
}