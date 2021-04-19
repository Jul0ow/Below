using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewShoot : MonoBehaviour
{
    private GameObject shootFrom;
    public GameObject bulletprefab;
    private float coeffForce = 4000f;
    public float fireRate;
    private float nextfire;
    public Camera cam;
    public Transform attackpoint;
    private PhotonView PV;



    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void fire()
    {
        
        if (Time.time > nextfire)
        {
            shootFrom = GameObject.Find("ShootFrom");
            nextfire = Time.time + fireRate;
            GameObject bullet = PhotonNetwork.Instantiate("PhotonPrefabs/" + bulletprefab.name, shootFrom.transform.position,
                Quaternion.identity, 0);
            bullet.GetComponent<projectiles>().owner = transform.root.gameObject;

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
            }
        }
    }
    

    private void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&& PV.IsMine)
        {
            fire();
        }
    }
}
