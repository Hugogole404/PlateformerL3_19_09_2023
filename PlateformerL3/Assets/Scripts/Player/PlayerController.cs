using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("SpawnPoint")]
    public GameObject _spawnPoint;
    [SerializeField] GameObject _player;

    [Header("Inputs")]
    private Vector2 _inputs;
    [SerializeField] bool _inputJump;
    [SerializeField] Rigidbody2D _rb;

    [Header("Movements")]
    public float _walkSpeed;
    public float _walkSpeedMemory;
    [SerializeField] float _acceleration;

    [Header("GroundCheck")]
    [SerializeField] float _groundOffset;
    [SerializeField] float _groundRadius;
    [SerializeField] LayerMask _GroundLayer;

    [Header("Jump")]
    [SerializeField] bool _isGrounded;
    [SerializeField] float _timeMinBetweenJump;
    [SerializeField] float _jumpForce;
    [SerializeField] float _velocityFallMin;
    [SerializeField][Tooltip("Gravity when the player goes up and press jump")] private float _gravityUpJump;
    [SerializeField] float _jumpInputTimer = 0.1f;
    [SerializeField] Collider2D[] _collidersGround;
    [SerializeField] float _timerNoJump;
    [SerializeField][Tooltip("Gravity otherwise")] private float _gravity;
    [SerializeField] float _timeSinceJumpPressed;
    [SerializeField] float _timeSinceGrounded;
    [SerializeField] float _coyoteTime;
    [SerializeField] PhysicsMaterial2D _physicsFriction;
    [SerializeField] PhysicsMaterial2D _physicsNoFriction;
    [SerializeField] int _jumpTankMax;
    [SerializeField] int _currentJumpTank;
    [SerializeField] AudioSource _jumpSoundOther;

    [Header("Slope")]
    [SerializeField] Collider2D _collider;
    private RaycastHit2D[] _hitResults = new RaycastHit2D[2];
    private float _slopeDetectOffset;
    [SerializeField] private bool _isOnSlope;

    [Header("Pause")]
    public bool _isPaused;

    [Header("Fly")]
    [SerializeField] bool _isFlying = false;
    [SerializeField] List<GameObject> _windArea = new List<GameObject>();
    [SerializeField] AudioSource FlySound;

    [Header("Ambiance Sounds")]
    [SerializeField] AudioSource BackgroundSong;
    [SerializeField] AudioSource AmbianceSounds;


    //[Header("Corner")]
    //[SerializeField] float[] direction;
    //[SerializeField] private BoxCollider2D _offsetCollisionBox;
    //[SerializeField] private BoxCollider2D _offsetToReplace;
    //[SerializeField] private BoxCollider2D offsetToReplace;
    //[SerializeField] private Vector2 _collisionBox;

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");
        _inputJump = Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _timeSinceJumpPressed = 0;
            _currentJumpTank--;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            for (int i = 0; i < _windArea.Count; i++)
            {
                //_isFlying = !_isFlying;
                //FlySound.Play();
                _windArea[i].SetActive(true);
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if (!FlySound.isPlaying)
                    {
                        FlySound.Play();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < _windArea.Count; i++)
            {
                _windArea[i].SetActive(false);
                FlySound.Stop();
            }
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
        bool currentGrounded = Physics2D.OverlapCircleNonAlloc(pointGround, _groundRadius, _collidersGround, _GroundLayer) > 0;

        if (currentGrounded == false && _isGrounded)
        {
            _timeSinceGrounded = 0;
        }

        _isGrounded = currentGrounded;

        if (_isGrounded)
        {
            _currentJumpTank = _jumpTankMax;
        }
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
            if (_rb.velocity.y <= 0 || _isOnSlope)
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

        if ((_inputJump && _rb.velocity.y <= 0 && (_isGrounded || _timeSinceGrounded < _coyoteTime) && _timerNoJump <= 0 && _timeSinceJumpPressed < _jumpInputTimer))
        {
            _jumpSoundOther.Play(0);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _timerNoJump = _timeMinBetweenJump;
        }
        else if ((_inputJump && _timeSinceJumpPressed < _jumpInputTimer) && !_isGrounded && _currentJumpTank > 0)
        {
            if(_currentJumpTank == 1)
                _jumpSoundOther.Play(0);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }


        // limiter vit chute : 
        if (_rb.velocity.y < _velocityFallMin)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _velocityFallMin);
        }

        _timeSinceJumpPressed += Time.deltaTime;
    }

    void HandleSlope()
    {
        Vector3 origin = transform.position + Vector3.up * _groundOffset;
        bool slopeRight = Physics2D.RaycastNonAlloc(origin, Vector2.right, _hitResults, _slopeDetectOffset, _GroundLayer) > 0;
        bool slopeLeft = Physics2D.RaycastNonAlloc(origin, -Vector2.right, _hitResults, _slopeDetectOffset, _GroundLayer) > 0;
        _isOnSlope = (slopeRight || slopeLeft) && (slopeRight == false || slopeLeft == false);
        if (Mathf.Abs(_inputs.x) < 00.1f && (slopeLeft || slopeRight))
        {
            _collider.sharedMaterial = _physicsFriction;
        }
        else
        {
            _collider.sharedMaterial = _physicsNoFriction;

        }
    }

    //void HandleCorners()
    //{
    //    for (int i = 0; i < direction.Length; i++)
    //    {
    //        float dir = direction[i];
    //        if (Mathf.Abs(_inputs.x) > 0.1f && Mathf.Abs(Mathf.Sign(dir) - Mathf.Sign(_inputs.x)) < 0.001f
    //            && _isGrounded == false && _isOnSlope == false)
    //        {
    //            Vector3 position = transform.position + new Vector3(_offsetCollisionBox.x + dir * _offsetToReplace.x,
    //                _offsetCollisionBox.y, 0);
    //            int result = Physics2D.BoxCastNonAlloc(position, _collisionBox, 0, Vector2.zero, _hitResults, 0, _GroundLayer);
    //            if (result > 0)
    //            {
    //                position = transform.position + new Vector3(_offsetCollisionBox.x + dir * _offsetToReplace.x, _offsetCollisionBox.y 
    //                    + offsetToReplace.y, 0);
    //                result = Physics2D.BoxCastNonAlloc(position, _collisionBox, 0, Vector2.zero, _hitResults, 0, _GroundLayer);
    //                if(result == 0)
    //                {
    //                    Debug.Log("replace");
    //                    transform.position += new Vector3(dir * _offsetToReplace.x, offsetToReplace.y);
    //                    if(_rb.velocity.y < 0)
    //                        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    //                }
    //            }
    //        }
    //    }
    //}

    private void Awake()
    {
        _player.transform.position = _spawnPoint.transform.position;
        _walkSpeedMemory = _walkSpeed;
    }
    private void Start()
    {
        BackgroundSong.Play();
        AmbianceSounds.Play();
    }

    private void Update()
    {
        if (!_isPaused)
        {
            HandleInputs();
        }
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleJump();
        HandleGrounded();
        HandleSlope();
    }
}