using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Jump : MonoBehaviour
{
    public Animator animator;
    public float jumpHeight;
    public bool IsGrounded = false;
    public bool Falling = false;
    public bool jumping = false;
    public Rigidbody rb;
    public int JumpCount;
    public CharacterController controller;
    public PhotonView PV;
    public float fallspeed;
    public float jumpheightref;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        fallspeed = 1;
        jumpheightref = jumpHeight;
    }

    protected virtual void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
 
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround-0.8f);
        if (!IsGrounded && !jumping)
        {
            Falling = true;
            controller.Move(Vector3.up * -fallspeed / 10);
            fallspeed += 0.03f;
            return;
        }
        else
        {
            Falling = false;
            fallspeed = 1;
        }
        if (IsGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                jumping = true;
                
            }
        }
        if (jumping)
        {
            controller.Move(Vector3.up * (jumpHeight/10));
            jumpHeight -= 0.02f;
            JumpCount -= 1;
            if (JumpCount == 0)
            {
                jumping = false;
                JumpCount = 20;
                jumpHeight = jumpheightref;
            }
        }
    }

    public virtual void ReduceFallSpeed()
    {
        fallspeed /= 1.5f;
    }
    
}
