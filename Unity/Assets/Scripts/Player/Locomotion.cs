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
    public float nextHit;
    public float attackRate = 0.4f;

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
        if (Time.time > nextHit && Input.GetButton("Fire2"))
        {
            nextHit = Time.time + attackRate;
            animator.SetBool("Attacking", true);
        }
        else
            animator.SetBool("Attacking", false);
    }
    
}
