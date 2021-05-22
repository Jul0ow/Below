using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator animator;
    public float WalkSpeed = 6f;
    public float RunSpeed = 16f;
    public float speed;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Component freeLook;
    public bool savon = false;


    private PhotonView PV;
    void Start()
    {
        animator = GetComponent<Animator>();
        float speed = WalkSpeed;
        PV = GetComponent<PhotonView>();

    }

    public void setToDeathPosition(Vector3 deathPosition)
    {
        controller.Move(deathPosition);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        if((animator.GetCurrentAnimatorStateInfo(0).IsName("Contact attack")))
                return;
        
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
            return;
        }
        freeLook.GetComponent<CinemachineFreeLook>().enabled = true;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey("left shift"))
                speed = RunSpeed;
            else
                if (speed >= WalkSpeed)
                    speed = WalkSpeed;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        
    }
}
