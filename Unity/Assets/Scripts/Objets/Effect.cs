using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static int Peg(GameObject Joueur)
    {
        Joueur.GetComponent<Movement>().WalkSpeed += 10;
        Joueur.GetComponent<Movement>().RunSpeed += 10;
        return 1;
    }
}
