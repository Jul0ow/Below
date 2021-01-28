using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    { 
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MaleDummy"), Vector3.zero, Quaternion.identity); //vector3 => position spawn
        //Instantiate our player controller
    }
}
