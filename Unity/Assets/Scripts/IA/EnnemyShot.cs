﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnnemyShot : MonoBehaviour
{
    public bool solo = false;
    public bool awake = false;
    public Rigidbody rb;
    public GameObject explosion;
    [Range(0f,1f)]
    public float bouciness;

    public bool useGravity;
    
    public float explosionRange;
    public int damage = 1;
    
    private PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    [PunRPC]
    public void explodeRpc(int view)
    {
        GameObject currentexplosion = PhotonView.Find(view).gameObject;
        Invoke("Delay", 0.05f);
        Invoke("DelayBoom(currentexplosion)", 0.5f);
    }


    public virtual void Explode()
    {
        if (explosion != null)
        {
            GameObject currentexplosion;
            if (solo)
            {
                currentexplosion = (GameObject) Instantiate(Resources.Load("PhotonPrefabs/" + explosion.name), transform.position, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere((currentexplosion).transform.position, explosionRange);
                for (int i = 0; i < enemies.Length; i++)
                {
                    // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                    if (enemies[i].CompareTag("Player"))
                    {
                        enemies[i].GetComponent<CharacterThings>().TakeDamage(damage, false, false);
                    }
                }
                Invoke("Delay", 0.05f);
                Invoke("DelayBoom(currentexplosion)", 0.5f);
            }
                
            else if (PhotonNetwork.IsMasterClient)
            {
                currentexplosion = PhotonNetwork.Instantiate("PhotonPrefabs/" + explosion.name, transform.position, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere((currentexplosion).transform.position, explosionRange);
                for (int i = 0; i < enemies.Length; i++)
                {
                    // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                    if (enemies[i].CompareTag("Player"))
                    {
                        enemies[i].GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,damage,false,false);
                    }
                }
                GetComponent<PhotonView>().RPC("explodeRpc", RpcTarget.All, currentexplosion.GetComponent<PhotonView>().ViewID);
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

    private void OnCollisionEnter(Collision collision)
    {
        //collisions++;
        //if(collision.collider.CompareTag("Enemy") && explodeOnTouche) 
        Explode();
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
