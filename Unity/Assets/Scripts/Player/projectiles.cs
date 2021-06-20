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
    public GameObject bulletprefab;
    [Range(0f,1f)]
    public float bouciness;

    public bool useGravity;

    public int Damage;
    public float explosionRange;
    public float explosionForce;
    
    public int maxCollisions;
    public float maxLifeTime;

    public int collisions;
    public PhysicMaterial physics_mat;
    public GameObject owner;
    public float coeffForce = 4000f;
    public bool isSplit = false;
    public bool Slowing = false;


    protected virtual void Start()
    {
        Setup();
        //PV = GetComponent<PhotonView>();
    }

    protected virtual void Update()
    {
        if(awake)
        {
            if (collisions > maxCollisions) 
                Explode();

            maxLifeTime -= Time.deltaTime;
            if (maxLifeTime <= 0) Explode();
        }
    }

    protected virtual void Explode()
    {
        if (explosion != null)
        {
            int tmp = Damage;
            
            if (owner.GetComponent<CharacterThings>().Souffrance)
            {
                tmp += owner.GetComponent<CharacterThings>().MaxHP - owner.GetComponent<CharacterThings>().HP;
            }
            
            tmp += owner.GetComponent<NewShoot>().BonusDamage;
            
            if (owner.GetComponent<CharacterThings>().dard)
            {
                tmp = 9999;
            }
            if (owner.GetComponent<CharacterThings>().killer)
            {
                tmp *= 3;
            }

            if (owner.GetComponent<CharacterThings>().bloodLove)
            {
                tmp *= 2;
            }

            if (owner.GetComponent<NewShoot>().Snipe)
            {
                tmp *= 1 + (int) (Time.time - owner.GetComponent<NewShoot>().timeSniped);
            }

            Slowing = owner.GetComponent<CharacterThings>().toile;
            
            GameObject currentexplosion;
            currentexplosion = PhotonNetwork.Instantiate("PhotonPrefabs/" + explosion.name, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(currentexplosion.transform.position, explosionRange);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Ennemy"))
                {
                    EnnemyLife IA = enemies[i].GetComponent<EnnemyLife>();
                    IA.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, tmp, Slowing);
                    if (owner.GetComponent<CharacterThings>().vampire && IA.Health <= 0)
                    {
                        owner.GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("Heal", RpcTarget.All, 10);
                    }
                }
                if (enemies[i].CompareTag("Player"))
                    if (enemies[i].gameObject != owner)
                    {
                        enemies[i].GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, tmp, Slowing, owner.GetComponent<CharacterThings>().purulence);
                    }
                if(enemies[i].GetComponent<Rigidbody>() && !enemies[i].CompareTag("Player") && !enemies[i].CompareTag("Ennemy"))
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            }
            Invoke("Delay", 0.05f);
            Invoke("DelayBoom(currentexplosion)", 0.5f);
            if (owner.GetComponent<CharacterThings>().dard)
            {
                owner.GetComponent<CharacterThings>().dard = false;
            }

            if (owner.GetComponent<NewShoot>().Arcanes && !isSplit)
            {
                GameObject bullet = PhotonNetwork.Instantiate("PhotonPrefabs/Bullet", transform.position,
                    Quaternion.identity, 0);
                GetComponent<PhotonView>().RPC("AppliedOwner", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID, bullet.GetComponent<PhotonView>().ViewID, true, 2);

                Vector3 direction = owner.transform.position - transform.position;
                
                Rigidbody body = bullet.GetComponent<Rigidbody>();
                transform.forward = direction;
                body.AddForce(transform.right * coeffForce);
                
                //------------------------------------------------------------------------------------
                bullet = PhotonNetwork.Instantiate("PhotonPrefabs/Bullet", transform.position,
                    Quaternion.identity, 0);
                GetComponent<PhotonView>().RPC("AppliedOwner", RpcTarget.All,
                    gameObject.GetComponent<PhotonView>().ViewID, bullet.GetComponent<PhotonView>().ViewID, true, 2);

                body = bullet.GetComponent<Rigidbody>();
                transform.forward = direction;
                body.AddForce(-transform.right * coeffForce);
            }
        }
    }
    
    protected virtual void DelayBoom(GameObject boom)
    {
        Destroy(boom);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (owner.GetComponent<NewShoot>().Infini)
        {
            float speed = rb.velocity.magnitude;
            Vector3 direction = Vector3.Reflect(rb.velocity.normalized, other.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(awake)
        {
            if (other.gameObject != owner)
            {
                if (owner.GetComponent<NewShoot>().Infini)
                {
                    if (other.TryGetComponent(typeof(CharacterThings), out _))
                    {
                        Explode();
                    }
                    else
                    {
                        float speed = rb.velocity.magnitude;
                        Vector3 direction = Vector3.Reflect(rb.velocity.normalized, transform.position - other.ClosestPoint(transform.position));

                        rb.velocity = direction * Mathf.Max(speed, 0f);
                    }
                    
                    
                }
                //Physics.IgnoreCollision(other, GetComponent<Collider>(), true);
                else
                    Explode();
            }
        }
    }
    
    protected virtual void Setup()
    {
        if (owner.GetComponent<NewShoot>().Infini)
        {
            bouciness = 1;
        }
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bouciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physics_mat;
        rb.useGravity = useGravity;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
