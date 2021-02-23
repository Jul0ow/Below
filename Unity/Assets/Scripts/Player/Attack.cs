using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range;
    public int Damage;
    public float power;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, range);
            for (int i = 0; i < enemies.Length; i++)
            {
                // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                if (enemies[i].GetComponent<Rigidbody>())
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
                if (enemies[i].CompareTag("Ennemy"))
                {
                    EnnemyIA IA = enemies[i].GetComponent<EnnemyIA>();
                    IA.TakeDamage(Damage);
                }
                if (enemies[i].CompareTag("Player"))
                {
                    CharacterThings victim = enemies[i].GetComponent<CharacterThings>();
                    if(victim != enemies[i].GetComponentInParent<CharacterThings>())
                        victim.TakeDamage(Damage);
                }
            }
        }
    }
}
