using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class SummonEnnemy : MonoBehaviour
{
    public bool elite = false;
    private static EnnemyList list;

    void Awake()
    {
        list = GetComponent<EnnemyList>();
        list.Creat();
        if (elite)
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickelite(), transform.position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickennemy(), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}