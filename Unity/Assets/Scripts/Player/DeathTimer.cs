using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public CharacterThings joueur;

    public TextMeshProUGUI text;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (joueur.isdead())
            text.text = "Vous êtes mort." + "\n" + "     " + Convert.ToString(Convert.ToInt32(joueur.deathTime + joueur.timeofDeath - Time.time));
        else
            text.text = "";
    }
}
