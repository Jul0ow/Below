using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class ShootingSystemSolo : MonoBehaviour
{
    /*public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, timeBetweenShot, spread, reloadTime;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    private int bulletsLeft, BulletsShot;

    public float recoilForce;
    public Rigidbody playerRB;

    private bool shooting, readyToShoot, reloading;

    public Camera cam;
    public Transform attackpoint;

    public ParticleSystem muzzleFlash;
    private TextMeshProUGUI ammunitionDisplay;

    public TextMeshProUGUI textAmmo;
    public Vector3 textAmmoposition;
    
    public bool allowInvoke = true;
    public Animator animator;
    private PhotonView PV;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        ammunitionDisplay = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
        textAmmo = Instantiate(ammunitionDisplay,textAmmoposition,Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        MyInput();
        if (textAmmo != null)
            if (bulletsLeft != 0)
                textAmmo.SetText("Fireballs : " + bulletsLeft + " / " + magazineSize);
            else
                textAmmo.SetText("Reloading...");
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetButton("Fire1");
        else shooting = Input.GetButtonDown("Fire1");
        
        if(Input.GetKeyDown("r") && bulletsLeft < magazineSize && !reloading) Reload();
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Contact attack"))
        {
            BulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit))
            targetpoint = hit.point;
        else
            targetpoint = ray.GetPoint(75);
        Vector3 directionWithoutSpread = targetpoint - attackpoint.position;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direactionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        GameObject currentBullet = Instantiate(bullet, attackpoint.position, Quaternion.identity);
        currentBullet.GetComponent<projectiles>().awake = true;
        currentBullet.transform.forward = direactionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(direactionWithSpread.normalized * shootForce, ForceMode.Impulse);
        if (muzzleFlash != null)
            muzzleFlash.Play();
        bulletsLeft--;
        BulletsShot++;
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
            playerRB.AddForce(-direactionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        if (BulletsShot < bulletPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShot);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }*/
}
