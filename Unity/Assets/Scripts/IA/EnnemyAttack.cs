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
    


    private void Awake()
    {
        IA = GetComponentInParent<EnnemyIA>();
        victim = IA.player;
    }


    private void Update()
    {
        victim = IA.player;
    }
    
    public void Shoot()
    {
        gameObject.transform.LookAt(victim.transform);
        Rigidbody currentbullet =
            PhotonNetwork.Instantiate("PhotonPrefabs/" + bullet.name, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        currentbullet.AddForce(transform.forward * shootForce,ForceMode.Impulse);
    }
}