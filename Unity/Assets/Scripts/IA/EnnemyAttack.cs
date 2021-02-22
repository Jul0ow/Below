using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAttack : MonoBehaviour
{
    public GameObject bullet;
    private GameObject victim;
    public int shootForce;
    private EnnemyIA IA;
    


    private void Awake()
    {
        IA = GetComponent<EnnemyIA>();
    }


    private void Update()
    {
        victim = IA.player;
    }
    
    public void Shoot()
    {
        gameObject.transform.LookAt(victim.transform);
        Rigidbody currentbullet =
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        currentbullet.AddForce(transform.forward * shootForce,ForceMode.Impulse);
    }
}
