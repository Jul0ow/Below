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
                Instantiate(Resources.Load("PhotonPrefabs/Mob/" + list.pickelite()) , transform.position, Quaternion.identity);
            else
                Instantiate(Resources.Load("PhotonPrefabs/Mob/" + list.pickennemy()), transform.position, Quaternion.identity);
        }
        else
        {
            if (elite)
                PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickelite(), transform.position, Quaternion.identity);
            else
                PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickennemy(), transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}