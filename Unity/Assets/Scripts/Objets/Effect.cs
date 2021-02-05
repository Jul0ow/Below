using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static int Peg(GameObject Joueur)
    {
        Joueur.GetComponent<Movement>().WalkSpeed += 5;
        Joueur.GetComponent<Movement>().RunSpeed += 5  ;
        return 1;
    }
}
