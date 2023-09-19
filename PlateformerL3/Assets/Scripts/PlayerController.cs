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

    [Header("Jump")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _groundRadius;
    [SerializeField] private Collider2D[] _collidersGround;
    [SerializeField] private LayerMask _GroundLayer;
    [SerializeField] private float _groundOffset;
    [SerializeField] private float _timerNoJump;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _timeMinBetweenJump;
    [SerializeField] private float _velocityFallMin;

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");
        _inputJump = Input.GetKey(KeyCode.UpArrow);
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
        if(_inputJump && _rb.velocity.y <=0 && _isGrounded && _timerNoJump <= 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _timerNoJump = _timeMinBetweenJump;
        }
        // limiter vit chute : 
        if(_rb.velocity.y < _velocityFallMin)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _velocityFallMin);
        }
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