using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class refresh : MonoBehaviour
{
    // Start is called before the first frame update
    public void Refresh()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings(); 
    }
}
