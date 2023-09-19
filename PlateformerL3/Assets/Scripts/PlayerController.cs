using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _inputs;
    private bool _inputJump;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] float velocity;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _acceleration;

    //private Vector2 _inputJump;
    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _inputJump = Input.GetKey(KeyCode.UpArrow);
    }
    void HandleMovement()
    {
        var velocity = _rb.velocity;
        Vector2 wantedVelocity = new Vector2(x:_inputs.x * _walkSpeed, velocity.y);
        _rb.velocity = Vector2.MoveTowards(current: velocity, target: wantedVelocity, maxDistanceDelta: _acceleration * Time.deltaTime);

    }
    private void Update()
    {
        HandleInputs();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
}