﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float WalkSpeed = 6f;
    public float RunSpeed = 16f;
    public float speed;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private PhotonView PV;
    void Start()
    {
        float speed = WalkSpeed;
        PV = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey("w"))
            {
                speed = RunSpeed;
            }
            else
            {
                if (speed >= WalkSpeed)
                {
                    speed = WalkSpeed;
                }
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
