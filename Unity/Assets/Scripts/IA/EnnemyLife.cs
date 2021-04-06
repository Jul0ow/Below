using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public AudioClip death;
    public int Health;
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            AudioSource.PlayClipAtPoint(death, GetComponent<Transform>().position);
            Destroy(gameObject);
        }
    }

    protected virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
