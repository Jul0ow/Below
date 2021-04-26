using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SummonDebug : MonoBehaviour
{
    public bool elite = false;
    public int x;

    /*public GameObject client;*/
    // Update is called once per frame
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            Destroy(gameObject);
        EnnemyList list = new EnnemyList();
        list = GetComponent<EnnemyList>();
        list.Creat();
        if (elite)
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickelite(x), transform.position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + list.pickennemy(x), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
