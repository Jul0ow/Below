using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BloodSummoner : MonoBehaviour
{
    public int cycle;

    void Start()
    {
        StartCoroutine(waiter());
    }
    // Update is called once per frame
    IEnumerator waiter()
    {
        GameObject blood = (GameObject) Instantiate(Resources.Load("PhotonPrefabs/Mob/Blood"), transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cycle);
        StartCoroutine(waiter());
    }
}
