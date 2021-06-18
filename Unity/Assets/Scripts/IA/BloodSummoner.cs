using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BloodSummoner : MonoBehaviour
{
    public int cycle;

    // Update is called once per frame
    void Update()
    {
        if(Time.time%cycle == 0)
            PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + "Blood", transform.position, Quaternion.identity);
    }
}
