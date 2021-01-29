using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static int Peg(GameObject Joueur)
    {
        Joueur.GetComponent<Movement>().RunSpeed += 30;
        return 1;
    }
}
