using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public bool torched = false;
    public GameObject torch;
    public float WalkSpeed = 6f;
    public float RunSpeed = 16f;
    public float SlowedRunSpeed = 7f;
    public bool slowed;
    public float slowedTime;
    public float speed;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Component freeLook;


    private PhotonView PV;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        float speed = WalkSpeed;
        PV = GetComponent<PhotonView>();
    }

    public virtual void setToDeathPosition(Vector3 deathPosition)
    {
        controller.Move(deathPosition);
    }
    
    
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!PV.IsMine)
            return;
        if (Input.GetKeyDown("f"))
        {
            if (torched)
            {
                torch.SetActive(false);
                torched = false;
            }
            else
            {
                torch.SetActive(true);
                torched = true;
            }
        }
        if (slowed)
        {
            if (slowedTime + 1.5f <= Time.time)
            {
                slowed = false;
                RunSpeed = 16;
            }
            else
            {
                RunSpeed = 7;
            }
            
            
        }
        if((animator.GetCurrentAnimatorStateInfo(0).IsName("Contact attack")))
                return;
        var CharacterRotation = cam.transform.rotation;
                 CharacterRotation.x = 0;
                 CharacterRotation.z = 0;
                 transform.rotation = CharacterRotation;
         
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
