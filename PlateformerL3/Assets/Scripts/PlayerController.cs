using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    private Vector2 _inputs;
    [SerializeField] private bool _inputJump;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movements")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _acceleration;

    [Header("GroundCheck")]
    [SerializeField] private float _groundOffset;
    [SerializeField] private float _groundRadius;
    [SerializeField] private LayerMask _GroundLayer;


    [Header("Jump")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _timeMinBetweenJump;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _velocityFallMin;
    [SerializeField] [Tooltip("Gravity when the player goes up and press jump")] private float _gravityUpJump;
    [SerializeField] private float _jumpInputTimer = 0.1f;
    [SerializeField] private Collider2D[] _collidersGround;
    [SerializeField] private float _timerNoJump;
    [SerializeField] [Tooltip("Gravity otherwise")] private float _gravity;
    [SerializeField] private float _timeSinceJumpPressed;
    [SerializeField] private float _timeSinceGrounded;

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");
        _inputJump = Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _timeSinceJumpPressed = 0;
        }
    }
    void HandleMovements()
    {
        var velocity = _rb.velocity;
        Vector2 wantedVelocity = new Vector2(_inputs.x * _walkSpeed, velocity.y);
        _rb.velocity = Vector2.MoveTowards(velocity, wantedVelocity, _acceleration * Time.deltaTime);
        //_rb.velocity = Vector2.MoveTowards(current: velocity, target: wantedVelocity, maxDistanceDelta: _acceleration * Time.deltaTime);
    }

    void HandleGrounded()
    {
        _timeSinceGrounded += Time.deltaTime;
        Vector2 pointGround = (transform.position + Vector3.up * _groundOffset);
        bool currentGrounded =
            Physics2D.OverlapCircleNonAlloc(pointGround, _groundRadius, _collidersGround, _GroundLayer) > 0;
        _isGrounded = currentGrounded;
    }


    private void OnDrawGizmos()
    {
        Vector2 pointGround = (transform.position + Vector3.up * _groundOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointGround, _groundRadius);
        Gizmos.color = Color.white;

    }

    void HandleJump()
    {
        _timerNoJump -= Time.deltaTime;
        if (_isGrounded == false)
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = _gravity;
            }
            else
            {
                _rb.gravityScale = _inputJump ? _gravityUpJump : _gravity;
            }
        }
        else
        {
            _rb.gravityScale = _gravity;
        }
        if (_inputJump && _rb.velocity.y <= 0 && _isGrounded && _timerNoJump <= 0 && 
            _timeSinceJumpPressed < _jumpInputTimer)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _timerNoJump = _timeMinBetweenJump;
        }


        // limiter vit chute : 
        if (_rb.velocity.y < _velocityFallMin)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _velocityFallMin);
        }
        _timeSinceJumpPressed += Time.deltaTime;
    }


    private void Update()
    {
        HandleInputs();
    }
    private void FixedUpdate()
    {
        HandleMovements();
        HandleJump();
        HandleGrounded();
    }
}