using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public BossIA Boss;
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        Collider[] getters = Physics.OverlapSphere(transform.position, 2);
        for (int i = 0; i < getters.Length; i++)
            if (getters[i].GetComponent<LifeScript>() && Input.GetKeyDown("e"))
            {
                Boss.Dead = true;
                source.Play();
                Destroy(this);
            }
    }
}
