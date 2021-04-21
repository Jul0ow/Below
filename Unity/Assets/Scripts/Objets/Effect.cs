using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static int Peg(GameObject Joueur)
    {
        Joueur.GetComponent<Movement>().WalkSpeed += 5;
        Joueur.GetComponent<Movement>().RunSpeed += 5 ;
        return 1;
    }

    public static int BloodLove(GameObject Joueur)
    {
        Joueur.GetComponentInChildren<Attack>().Damage *=3;
        Joueur.GetComponent<CharacterThings>().armor /= 5;
        return 1;
    }

    public static int theRing(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().theRing = true;
        return 1;
    }

    public static int Basket(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().basketpeg = true;
        return 1;
    }

    public static int Ticket(GameObject Joueur)
    {
        for (int i = 0; i < 3; i++)
        {
            Classes.Item content;
            uint ItemReference = (uint) Random.Range(0, Classes.AllItem[i].Count);
            content = Classes.AllItem[i][ItemReference];
            if (content is Classes.passive)
            {
                content.Joueur = Joueur;
                content.AppliedEffect();
                Joueur.GetComponent<CharacterThings>().Inventory.Add(content);
            }
            else
            {
                i--;
            }
        }
        return 1;
    }

    public static int Dard(GameObject Joueur)
    {
        return 1;
    }

    public static int KillerInstinct(GameObject Joueur)
    {
        Joueur.GetComponentInChildren<Attack>().Damage *=3;
        return 1;
    }

    public static int Lynx(GameObject Joueur)
    {
        Screen.brightness += 0.3f;
        return 1;
    }

    public static int OneUp(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().OneUp = true;
        return 1;
    }

    public static int cécité(GameObject Joueur)
    {
        Screen.brightness -= 0.3f;
        return 1;
    }

    public static int trèfle(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().luck += 1;
        return 1;
    }

    public static int savon(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().savon = true;
        return 1;
    }

    public static int plume(GameObject Joueur)
    {
        Joueur.GetComponent<Jump>().ReduceFallSpeed();
        return 1;
    }

    public static int poulet(GameObject Joueur)
    {
        if (Joueur.GetComponent<LifeScript>().HP == Joueur.GetComponent<LifeScript>().MaxHP)
        {
            Joueur.GetComponent<LifeScript>().MaxHP += 25;
        }
        else
        {
            Joueur.GetComponent<LifeScript>().HP = Joueur.GetComponent<LifeScript>().MaxHP;
        }
        return 1;
    }

    public static int Maille(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().armor += 10;
        return 1;
    }

    public static int Bouclier(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().armor += 20;
        return 1;
    }

    public static int Vampirisme(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().vampire = true;
        return 1;
    }

    public static int Cape(GameObject Joueur)
    {
        Joueur.tag = "Caped";
        return 1;
    }

}