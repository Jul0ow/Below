using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class SummonEnnemy : MonoBehaviour
{
    public bool elite = false;
    public bool solo = false;
    private Object mob;

    void Awake()
    {
    }
    
    public void Summon()
    {
        EnnemyList list = gameObject.AddComponent<EnnemyList>();
        list = GetComponent<EnnemyList>();
        list.Creat();
        if (solo)
        {
            if (elite)
                mob = Instantiate(Resources.Load("PhotonPrefabs/Mob/" + list.pickelite()) , transform.position, Quaternion.identity);
            else
                mob = Instantiate(Resources.Load("PhotonPrefabs/Mob/" + list.pickennemy()), transform.position, Quaternion.identity);
            ((GameObject) mob).GetComponent<EnnemyIA>().solo = solo;
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (elite)
                    mob = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickelite(), transform.position, Quaternion.identity);
                else
                    mob = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickennemy(), transform.position, Quaternion.identity);
            }
        }
       
        if (!solo && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        if (solo)
        {
            Destroy(gameObject);
        }
        
    }
}