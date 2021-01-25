using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight;
    public bool isGrounded;
    public bool jumping = false;
    public Rigidbody rb;
    public int JumpCount = 60;
    public CharacterController controller;
    
    void Update()
    {
        if (controller.velocity.y == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                jumping = true;
            }
        }
        if (jumping)
        {
            rb.useGravity = false;
            controller.Move(Vector3.up * (jumpHeight/20));
            jumpHeight -= 0.01f;
            JumpCount -= 1;
            if (JumpCount == 0)
            {
                jumping = false;
                rb.useGravity = true;
                JumpCount = 60;
                jumpHeight = 1f;
            }
        }
    }
    
}
