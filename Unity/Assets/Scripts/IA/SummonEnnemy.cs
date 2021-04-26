using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class SummonEnnemy : MonoBehaviour
{
    public bool elite = false;

    void Awake()
    {
        EnnemyList list = new EnnemyList();
        list = GetComponent<EnnemyList>();
        list.Creat();
        if (elite)
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickelite(), transform.position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickennemy(), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}