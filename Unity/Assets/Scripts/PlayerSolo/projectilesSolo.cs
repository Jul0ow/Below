using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.Encryption;
using UnityEngine;
using TMPro;

public class projectilesSolo : projectiles
{
    protected override void Start()
    {
        Setup();
        //PV = GetComponent<PhotonView>();
    }

    protected override void Update()
    {
        if(awake)
        {
            if (collisions > maxCollisions) 
                Explode();

            maxLifeTime -= Time.deltaTime;
            if (maxLifeTime <= 0) Explode();
        }
    }

    protected override void Explode()
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
            currentexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(currentexplosion.transform.position, explosionRange);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Ennemy"))
                {
                    EnnemyLife IA = enemies[i].GetComponent<EnnemyLife>();
                    IA.TakeDamage(tmp, Slowing);
                    if (owner.GetComponent<CharacterThings>().vampire && IA.Health <= 0)
                    {
                        owner.GetComponent<CharacterThings>().Heal(10);
                    }
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
                GameObject bullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);
                bullet.GetComponent<projectilesSolo>().owner = owner;
                bullet.GetComponent<projectilesSolo>().awake = true;
                bullet.GetComponent<projectilesSolo>().isSplit = true;
                bullet.GetComponent<projectilesSolo>().Damage /= 2;
                Vector3 direction = owner.transform.position - transform.position;
                
                Rigidbody body = bullet.GetComponent<Rigidbody>();
                transform.forward = direction;
                body.AddForce(transform.right * coeffForce);
                
                //------------------------------------------------------------------------------------
                bullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);
                bullet.GetComponent<projectilesSolo>().owner = owner;
                bullet.GetComponent<projectilesSolo>().awake = true;
                bullet.GetComponent<projectilesSolo>().isSplit = true;
                bullet.GetComponent<projectilesSolo>().Damage /= 2;
                body = bullet.GetComponent<Rigidbody>();
                transform.forward = direction;
                body.AddForce(-transform.right * coeffForce);
            }
        }
    }
    
    protected override void DelayBoom(GameObject boom)
    {
        Destroy(boom);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (owner.GetComponent<NewShoot>().Infini)
        {
            float speed = rb.velocity.magnitude;
            Vector3 direction = Vector3.Reflect(rb.velocity.normalized, other.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(awake)
        {
            if (other.gameObject != owner)
            {
                if (owner.GetComponent<NewShootSolo>().Infini)
                {
                    if (other.TryGetComponent(typeof(CharacterThingSolos), out _) || other.TryGetComponent(typeof(EnnemyIA), out _))
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
    
    protected override void Setup()
    {
        if (owner.GetComponent<NewShootSolo>().Infini)
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

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
