using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnnemyAttack : MonoBehaviour
{
    public GameObject bullet;
    private GameObject victim;
    public int shootForce;
    private EnnemyIA IA;
    public int damage;
    public AudioSource fireball;

    private void Awake()
    {
        IA = GetComponentInParent<EnnemyIA>();
        if(IA!=null) victim = IA.player;
    }


    private void Update()
    {
        if(IA!=null) victim = IA.player;
    }
    
    public void Shoot()
    {
        if(victim==null) return;
        fireball.Play();
        gameObject.transform.LookAt(victim.transform);
        Rigidbody currentbullet =
            PhotonNetwork.Instantiate("PhotonPrefabs/" + bullet.name, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        currentbullet.GetComponent<EnnemyShot>().damage = damage;
        currentbullet.AddForce(transform.forward * shootForce,ForceMode.Impulse);
    }
}