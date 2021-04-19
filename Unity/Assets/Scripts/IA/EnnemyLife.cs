using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public EnnemyIA IA;
    public AudioClip death;
    public int Health;
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            AudioSource.PlayClipAtPoint(death, GetComponent<Transform>().position);
            DestroyEnemy();
        }
    }

    protected virtual void DestroyEnemy()
    {
        if(IA is BlobIA ia) ia.Deathrattle();
        else Destroy(gameObject);
    }
}
