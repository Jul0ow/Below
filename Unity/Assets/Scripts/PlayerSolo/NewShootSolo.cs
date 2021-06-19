using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewShootSolo : MonoBehaviour
{
    public GameObject player;
    private GameObject shootFrom;
    public GameObject bulletprefab;
    private float coeffForce = 4000f;
    public float fireRate;
    private float nextfire;
    public Camera cam;
    public Transform attackpoint;
    public AudioSource fireballSound;
    public bool Infini;
    public bool Snipe;
    public float timeSniped;
    public bool Arcanes;
    public int BonusDamage;



    private void Start()
    {
        player = gameObject;
    }

    public void fire()
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


    private void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&& !player.GetComponent<MovementSolo>().torched)
        {
            fire();
        }
    }
}
