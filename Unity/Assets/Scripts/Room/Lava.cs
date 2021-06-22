using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public bool solo = false;

    
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !collider.gameObject.GetComponent<CharacterThings>().Pastille)
        {
            if (solo)
            {
                collider.GetComponent<CharacterThings>().TakeDamage(100);
            }
            else
            {
                collider.gameObject.GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 100, false, false);
            }
        }    
    }
    
}
