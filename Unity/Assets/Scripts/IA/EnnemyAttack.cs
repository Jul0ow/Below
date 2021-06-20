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
    public bool solo = false;
    private Rigidbody currentbullet;

    private void Awake()
    {
        IA = GetComponentInParent<EnnemyIA>();
        if(IA!=null) victim = IA.player;
        solo = IA.gameObject.GetComponent<EnnemyIA>().solo;
    }


    private void Update()
    {
        solo = IA.gameObject.GetComponent<EnnemyIA>().solo;
        if(IA!=null) victim = IA.player;
    }
    
    public void Shoot()
    {
        if(victim==null) return;
        fireball.Play();
        gameObject.transform.LookAt(victim.transform);
        if (solo)
        {
            object bulletinstance = Instantiate(Resources.Load("PhotonPrefabs/" + bullet.name), transform.position,
                Quaternion.identity);
            currentbullet = ((GameObject) bulletinstance).GetComponent<Rigidbody>();
        }
        else
        {
            currentbullet = PhotonNetwork.Instantiate("PhotonPrefabs/" + bullet.name, transform.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
        }
        currentbullet.gameObject.GetComponent<EnnemyShot>().solo = solo;
        currentbullet.GetComponent<EnnemyShot>().damage = damage;
        currentbullet.AddForce(transform.forward * shootForce,ForceMode.Impulse);
    }
}