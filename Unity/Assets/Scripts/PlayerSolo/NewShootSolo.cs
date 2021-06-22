using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewShootSolo : NewShoot
{
    protected override void Start()
    {
        //player = gameObject;
    }

    public override void fire()
    {
        if (Time.time > nextfire)
        {
            shootFrom = GameObject.Find("ShootFrom");
            nextfire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletprefab, shootFrom.transform.position, Quaternion.identity);
            bullet.GetComponent<projectilesSolo>().owner = gameObject;
            bullet.GetComponent<projectilesSolo>().awake = true;
            bullet.GetComponent<projectilesSolo>().isSplit = false;
            bullet.GetComponent<projectilesSolo>().Damage /= 1;
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


    protected override void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&& !GetComponent<MovementSolo>().torched)
        {
            fire();
        }
    }
}
