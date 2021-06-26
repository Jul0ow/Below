using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DestroyBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);
    }
}
