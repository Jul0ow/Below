using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionSolo : Locomotion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementSolo>();
        jump = GetComponent<JumpSolo>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Current attack"))
            return;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
        if (Input.GetKey("left shift") & movement.speed > movement.WalkSpeed)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        animator.SetBool("Grounded", jump.IsGrounded);
        animator.SetBool("Falling", jump.Falling);
        animator.SetBool("Jumping", jump.jumping);
        if (Time.time > nextHit && Input.GetButton("Fire2") && !movement.torched) 
        {
            nextHit = Time.time + attackRate;
            animator.SetBool("Attacking", true);
        }
        else
            animator.SetBool("Attacking", false);
    }
    
}
