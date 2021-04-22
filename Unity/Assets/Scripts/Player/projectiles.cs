using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.Encryption;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class projectiles : MonoBehaviour
{
    public bool awake = false;
    public Rigidbody rb;
    public GameObject explosion;
    [Range(0f,1f)]
    public float bouciness;

    public bool useGravity;

    public int Damage;
    public float explosionRange;
    public float explosionForce;
    
    public int maxCollisions;
    public float maxLifeTime;

    private int collisions;
    private PhysicMaterial physics_mat;
    
    public GameObject owner;


    private void Start()
    {
        Setup();
        //PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(awake)
        {
            if (collisions > maxCollisions) 
                Explode();

            maxLifeTime -= Time.deltaTime;
            if (maxLifeTime <= 0) Explode();
        }
    }

    void Explode()
    {
        if (explosion != null)
        {
            int tmp = Damage;
            if (owner.GetComponent<CharacterThings>().dard)
            {
                Damage = 9999;
            }
            GameObject currentexplosion;
            currentexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(currentexplosion.transform.position, explosionRange);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Ennemy"))
                {
                    EnnemyIA IA = enemies[i].GetComponent<EnnemyIA>();
                    IA.TakeDamage(Damage);
                    if (owner.GetComponent<CharacterThings>().vampire && IA.Health <= 0)
                    {
                        owner.GetComponent<CharacterThings>().HP += 10;
                    }
                }

                if (enemies[i].CompareTag("Player"))
                {
                    /*CharacterThings victim = enemies[i].GetComponent<CharacterThings>();
                    if(victim != enemies[i].GetComponentInParent<CharacterThings>())
                        victim.TakeDamage(Damage);*/
                    if (enemies[i].gameObject != owner)
                    {
                        enemies[i].GetComponent<CharacterThings>().TakeDamage(Damage);
                    }

                    
                }
                if(enemies[i].GetComponent<Rigidbody>())
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);

            }
            Invoke("Delay", 0.05f);
            Invoke("DelayBoom(currentexplosion)", 0.5f);
            if (owner.GetComponent<CharacterThings>().dard)
            {
                Damage = tmp;
                owner.GetComponent<CharacterThings>().dard = false;
            }
        }
    }
    
    private void DelayBoom(GameObject boom)
    {
        Destroy(boom);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner)
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>(), true);
        }
        else
        {
            Explode();
        }
    }
    
    private void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bouciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physics_mat;
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
