﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewShoot : MonoBehaviour
{
    public GameObject player;
    public GameObject shootFrom;
    public GameObject bulletprefab;
    public float coeffForce = 4000f;
    public float fireRate;
    public float nextfire;
    public Camera cam;
    public Transform attackpoint;
    public PhotonView PV;
    public AudioSource fireballSound;
    public bool Infini;
    public bool Snipe;
    public float timeSniped;
    public bool Arcanes;
    public int BonusDamage;



    protected virtual void Start()
    {
        player = gameObject;
        PV = GetComponent<PhotonView>();
    }

    public virtual void fire()
    {
        if (Time.time > nextfire)
        {
            shootFrom = GameObject.Find("ShootFrom");
            nextfire = Time.time + fireRate;
            GameObject bullet = PhotonNetwork.Instantiate("PhotonPrefabs/" + bulletprefab.name, shootFrom.transform.position,
                Quaternion.identity, 0);
            GetComponent<PhotonView>().RPC("AppliedOwner", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID, bullet.GetComponent<PhotonView>().ViewID, false, 1);

            if (cam != null)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5f, 0));
                RaycastHit hit;
                Vector3 targetpoint;
                if (Physics.Raycast(ray, out hit))
                    targetpoint = hit.point;
                else
                    targetpoint = ray.GetPoint(75);
                
                Vector3 directionWithoutSpread = targetpoint - attackpoint.position;

                Rigidbody body = bullet.GetComponent<Rigidbody>();
                shootFrom.transform.forward = directionWithoutSpread;
                body.AddForce(shootFrom.transform.forward * coeffForce);
                fireballSound.Play();
                if (Snipe)
                {
                    timeSniped = Time.time;
                }
            }
        }
    }

    [PunRPC]
    public virtual void AppliedOwner(int owner, int bulletview, bool isSplit, int division)
    {
        GameObject bullet =  PhotonView.Find(bulletview).gameObject;
        bullet.GetComponent<projectiles>().owner = PhotonView.Find(owner).gameObject;
        bullet.GetComponent<projectiles>().awake = true;
        bullet.GetComponent<projectiles>().isSplit = isSplit;
        bullet.GetComponent<projectiles>().Damage /= division;
    }
    

    protected virtual void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&& PV.IsMine && !player.GetComponent<Movement>().torched)
        {
            fire();
        }
    }
}
