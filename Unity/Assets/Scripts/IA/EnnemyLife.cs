using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnnemyLife : MonoBehaviour
{
    public EnnemyIA IA;
    public int Health;
    public AudioSource death;
    private bool solo;

    public virtual void Awake()
    {
        solo = gameObject.GetComponent<EnnemyIA>().solo;
    }
    
    [PunRPC]
    public virtual void TakeDamage(int damage, bool slowed = false)
    {
        Health -= damage;
        if (slowed && GetComponent<EnnemyIA>().speed > 1)
        {
            GetComponent<EnnemyIA>().speed -= 1;
        }
        if (Health <= 0)
        {
            death.Play();
            DestroyEnemy();
        }
    }

    protected virtual void DestroyEnemy()
    {
        solo = gameObject.GetComponent<EnnemyIA>().solo;
        if(IA.IsElite)
        {
            if (solo)
            {
                 GameObject chest = (GameObject) Instantiate(Resources.Load("PhotonPrefabs/chestInGame"), transform.position, Quaternion.identity);
                 chest.GetComponent<test>().solo = true;
            }
                
            else if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("PhotonPrefabs/chestInGame", transform.position, Quaternion.identity);
            }
                
        }
        if(IA is BlobIA ia) ia.Deathrattle();
        else if(IA is MimiqueIA ia2) ia2.Deathrattle();
        else
        {
            if(solo)
                Destroy(gameObject);
            else if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
