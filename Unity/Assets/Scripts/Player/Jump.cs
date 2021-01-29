using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Jump : MonoBehaviour
{
    Animator animator;
    public float jumpHeight;
    public bool IsGrounded = false;
    public bool jumping = false;
    public Rigidbody rb;
    public int JumpCount = 80;
    public CharacterController controller;
    private PhotonView PV;

    void Start()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
 
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround-0.818f);
        if (!IsGrounded && !jumping)
        {
            controller.Move(Vector3.up * -jumpHeight / 2);
            return;
        }
        animator.SetFloat("JumpHeight", jumpHeight);
        
        if (IsGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                jumping = true;
                
            }
        }
        if (jumping)
        {
            rb.useGravity = false;
            controller.Move(Vector3.up * (jumpHeight/10));
            jumpHeight -= 0.02f;
            JumpCount -= 1;
            if (JumpCount == 0)
            {
                jumping = false;
                rb.useGravity = true;
                JumpCount = 80;
                jumpHeight = 1f;
            }
        }
    }
    
}
