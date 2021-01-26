using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    CharacterController controller;
    Vector2 input;
    Movement movement;
    Jump jump;
    int NeedToLand = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
        jump = GetComponent<Jump>();
        if (jump.IsGrounded == false)
        {
            animator.Play("Falling");
            NeedToLand = 50;
        }
        else
        {
            if (NeedToLand != 0)
            {
                animator.Play("Landing");
                NeedToLand -= 1;
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
                        movement = GetComponent<Movement>();
                        if (Input.GetKey("w") & movement.speed == movement.RunSpeed)
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
