using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _inputJump = Input.GetKey(KeyCode.UpArrow);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}
