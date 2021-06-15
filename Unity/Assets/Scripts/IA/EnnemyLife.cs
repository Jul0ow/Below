using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public EnnemyIA IA;
    public int Health;
    public AudioSource death;
    
    [PunRPC]
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            death.Play();
            DestroyEnemy();
        }
    }

    protected virtual void DestroyEnemy()
    {
        if(IA.IsElite) PhotonNetwork.Instantiate("PhotonPrefabs/chestInGame", transform.position, Quaternion.identity);
        if(IA is BlobIA ia) ia.Deathrattle();
        else if(IA is MimiqueIA ia2) ia2.Deathrattle();
        else PhotonNetwork.Destroy(gameObject);
    }
}
