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
            if (getters[i].GetComponent<CharacterThingSolos>())
            {
                getters[i].transform.position = new Vector3(676, -96,-724);
                //Boss.SetActive(true);
            }
    }
}
