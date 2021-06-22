using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public BossIA Boss;
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        Collider[] getters = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < getters.Length; i++)
            if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown(KeyCode.E))
            {
                Boss.Dead = true;
                Boss.animator.SetBool("Jump", false);
                Boss.animator.SetBool("Die",true);
                source.Play();
                Destroy(gameObject);
            }
    }
}
