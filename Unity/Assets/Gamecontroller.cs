using Photon.Pun;
using UnityEngine;

public class Gamecontroller : MonoBehaviourPun
{
    private void Awake()
    {
        PhotonNetwork.Instantiate("man1",Vector3.zero,Quaternion.identity);
    }
    
}
