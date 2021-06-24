using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnnemyShotTox : EnnemyShot
{

    [PunRPC]
    public void poison(int viewID)
    {
        GameObject player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<CharacterThings>().poisoned = true;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        
    }

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
                    if (solo)
                    {
                        enemies[i].gameObject.GetComponent<CharacterThings>().poisoned = true;
                        Destroy(gameObject);
                        Invoke("Delay", 0.05f);
                        Invoke("DelayBoom(currentexplosion)", 0.5f);
                    }
                    else if (PhotonNetwork.IsMasterClient)
                    {
                        gameObject.GetComponent<PhotonView>().RPC("poison", RpcTarget.All, enemies[i].gameObject.GetComponent<PhotonView>().ViewID);
                        Invoke("Delay", 0.05f);
                        Invoke("DelayBoom(currentexplosion)", 0.5f);
                    }
                }
            }
            
        }
    }
    
    
    
}
