using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Options : MonoBehaviour
{
    public void options()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public void returnmenu()
    {
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.Disconnect();
    }
    
    public void sound()
    {
        
    }
}
