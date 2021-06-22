using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Collider[] getters = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < getters.Length; i++)
            if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown(KeyCode.E))
            {
                getters[i].GetComponent<CharacterThings>().Heal(99999);
            }
    }
}
