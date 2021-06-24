using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class openingdoor : MonoBehaviour
{
    //public GameObject theDoor;
    public float timeBeforOpeningThedoor;
    private float startTime;
    private Animator animator;
    public AudioSource sound;

    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        opened = false;
        animator = gameObject.GetComponent<Animator>();
        /* Vector3 spawnPosition = spawner.transform.position;
        Debug.Log(spawnPosition);
        PhotonNetwork.Instantiate("PhotonPrefabs/Rooms/the door",spawnPosition, Quaternion.identity);
        */
        //Debug.Log(animator);
    }

// Update is called once per frame
    void Update()
    {
        if (!opened)
        {
            if (Time.timeSinceLevelLoad >= startTime + timeBeforOpeningThedoor)
            {
                sound.Play();
                //PhotonNetwork.Destroy(theDoor);
                animator.SetBool("open",true);
                opened = true;
                //Debug.Log(Time.time - startTime);
                //Debug.Log(animator.parameters[0].);
            }
        }
        
    }
}
