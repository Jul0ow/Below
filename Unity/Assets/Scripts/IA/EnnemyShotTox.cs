using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShotTox : EnnemyShot
{


    public override void Explode()
    {
        if (explosion != null)
        {
            GameObject currentexplosion;
            currentexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Collider[] enemies = Physics.OverlapSphere(currentexplosion.transform.position, explosionRange);
            for (int i = 0; i < enemies.Length; i++)
            {
                // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                if (enemies[i].CompareTag("Player"))
                {
                    enemies[i].gameObject.GetComponent<CharacterThings>().poisoned = true;
                    Destroy(gameObject);
                }
            }
            Invoke("Delay", 0.05f);
            Invoke("DelayBoom(currentexplosion)", 0.5f);
        }
    }
    
    
    
}
