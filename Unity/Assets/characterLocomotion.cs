using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.Setfloat("InputX", input.x);
        animator.Setfloat("InputY", input.y);
    }
}
