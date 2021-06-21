using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSolo : Attack
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }

        if (Time.time > nextHit && Input.GetButtonDown("Fire2") && !movement.torched) 
        {
            woosh.Play();
            nextHit = Time.time + attackRate;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range);
            int tmp = Damage;
            if (GetComponentInParent<CharacterThings>().dard)
            {
                tmp = 9999;
            }
            if (GetComponentInParent<CharacterThings>().Souffrance)
            {
                tmp += GetComponent<CharacterThings>().MaxHP - GetComponent<CharacterThings>().HP;
            }
            if (GetComponentInParent<CharacterThings>().killer)
            {
                tmp *= 3;
            }

            if (GetComponentInParent<CharacterThings>().bloodLove)
            {
                tmp *= 2;
            }
            for (int i = 0; i < enemies.Length; i++)
            {
                // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                if (enemies[i].GetComponent<Rigidbody>() && !enemies[i].CompareTag("Player") && !enemies[i].CompareTag("Ennemy"))
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
                if (enemies[i].CompareTag("Ennemy") && enemies[i].TryGetComponent(typeof(Rigidbody), out _))
                {
                    if (Prot)
                    {
                        enemies[i].attachedRigidbody.AddExplosionForce(4000f, transform.position, range);
                    }
                    EnnemyLife IA = enemies[i].GetComponent<EnnemyLife>();
                    IA.TakeDamage(tmp);
                }
                if (enemies[i].CompareTag("Player"))
                {
                    CharacterThingSolos victim = enemies[i].GetComponent<CharacterThingSolos>();
                    
                    if (victim != enemies[i].GetComponentInParent<CharacterThingSolos>())
                    {
                        if (Prot)
                        {
                            enemies[i].attachedRigidbody.AddExplosionForce(4000f, transform.position, range);
                        }

                        victim.TakeDamage(tmp,GetComponentInParent<CharacterThingSolos>().toile, GetComponentInParent<CharacterThingSolos>().purulence); 
                    }
                    
                    
                }
            }
        }
    }
}
