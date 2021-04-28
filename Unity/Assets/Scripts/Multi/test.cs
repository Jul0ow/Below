using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviour
{
    public GameObject mob;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + mob.name, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
