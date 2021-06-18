using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Collider[] getters = Physics.OverlapSphere(transform.position, 5);
        for (int i = 0; i < getters.Length; i++)
            if (getters[i].GetComponent<LifeScript>() && Input.GetKeyDown("e"))
            {
                getters[i].GetComponent<LifeScript>().HP = getters[i].GetComponent<LifeScript>().MaxHP;
            }
    }
}
