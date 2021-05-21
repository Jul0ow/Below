using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range;
    public int Damage;
    public float power;
    public Animator animator;
    public float nextHit;
    public float attackRate = 0.4f;
    public AudioSource woosh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }
        
        if (Time.time > nextHit && Input.GetButtonDown("Fire2"))
        {
            woosh.Play();
            nextHit = Time.time + attackRate;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range);
            int tmp = Damage;
            if (GetComponentInParent<CharacterThings>().dard)
            {
                tmp = 9999;
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
                if (enemies[i].GetComponent<Rigidbody>())
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
                if (enemies[i].CompareTag("Ennemy"))
                {
                    EnnemyLife IA = enemies[i].GetComponent<EnnemyLife>();
                    IA.TakeDamage(tmp);
                }
                if (enemies[i].CompareTag("Player"))
                {
                    CharacterThings victim = enemies[i].GetComponent<CharacterThings>();
                    if(victim != enemies[i].GetComponentInParent<CharacterThings>())
                        victim.TakeDamage(tmp);
                }
            }
        }
    }
}
