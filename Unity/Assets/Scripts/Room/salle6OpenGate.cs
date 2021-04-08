using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class salle6OpenGate : MonoBehaviour
{
    public GameObject theDoor;
    public float timeBeforOpeningThedoor;
    private float startTime;

    private bool destroyed;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        destroyed = false;
        /* Vector3 spawnPosition = spawner.transform.position;
        Debug.Log(spawnPosition);
        PhotonNetwork.Instantiate("PhotonPrefabs/Rooms/the door",spawnPosition, Quaternion.identity);
        */
    }

// Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + timeBeforOpeningThedoor && !destroyed)
        {
            PhotonNetwork.Destroy(theDoor);
            destroyed = true;
        }
    }
}
