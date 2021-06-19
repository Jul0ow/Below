using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    private Vector3 pos;
    public GameObject player;
    
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        pos = GetComponent<Transform>().position;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    public void CreateController()
         { 
             //Debug.Log(pos);
             PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MaleDummy"), pos , Quaternion.identity);
             
             //Instantiate our player controller
         }
    
    public void CreateControllerSolo()
    {
        GameObject maledummy = Instantiate(player, pos , Quaternion.identity);
    }
}
