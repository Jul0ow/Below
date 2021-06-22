using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portail : MonoBehaviour
{
    public GameObject Boss;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] getters = Physics.OverlapSphere(transform.position, 5);
        for (int i = 0; i < getters.Length; i++)
            if (getters[i].GetComponent<CharacterThingSolos>() && Input.GetKeyDown(KeyCode.E))
            {
                getters[i].GetComponent<CharacterController>().enabled = false;
                getters[i].transform.position = new Vector3(676f, -100f,-724f);
                getters[i].GetComponent<CharacterController>().enabled = true;
                Boss.SetActive(true);
            }
    }
}
