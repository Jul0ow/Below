using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Photon.Pun;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    CharacterController controller;
    Vector2 input;
    Movement movement;
    Jump jump;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        PV = GetComponent<PhotonView>();

        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if((animator.GetCurrentAnimatorStateInfo(0).IsName("Contact attack")))
            return;
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play("Contact attack");
        }
        jump = GetComponent<Jump>();
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        animator.SetBool("Grounded", jump.IsGrounded);
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") |
             animator.GetCurrentAnimatorStateInfo(0).IsName("JumpStart")))
        {
            if (jump.IsGrounded & rb.useGravity)
            {
                animator.Play("Landing");
                return;
            }
        }
        if (jump.IsGrounded == false & jump.jumping == false)
        {
                animator.Play("Falling");
        }
        else
        {
            if (jump.jumping == false)
            {
                        if (Input.GetKeyDown("space"))
                        {
                            animator.Play("JumpStart");
                        }
                        else
                        {
                            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Landing") == false)
                            {
                                movement = GetComponent<Movement>();
                                if (Input.GetKey("w") & movement.speed > movement.WalkSpeed)
                                {
                                    animator.Play("Run");
                                    movement.speed = movement.WalkSpeed;
                                }
                                else
                                {
                                    animator.Play("Locomotion");
                                }
                            }
                        }
            }
                
        }
    }
}
