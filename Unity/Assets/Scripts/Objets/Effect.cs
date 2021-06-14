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
        Joueur.GetComponent<CharacterThings>().bloodLove = true;
        return 1;
    }

    public static int theRing(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().ring = true;
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
            int ItemReference = Random.Range(0, Classes.AllItem[i].Count);
            content = Classes.AllItem[i][ItemReference];
            if (content is Classes.passive)
            {
                content.Joueur = Joueur;
                content.AppliedEffect();
                //Joueur.GetComponent<CharacterThings>().Inventory.Add(content);
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
        Joueur.GetComponent<CharacterThings>().dard = true;
        return 1;
    }

    public static int KillerInstinct(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().killer = true;
        return 1;
    }

    public static int Lynx(GameObject Joueur)
    {
        Joueur.GetComponentInChildren<Light>().enabled = true;
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
        Joueur.GetComponent<Movement>().savon = true;
        return 1;
    }

    public static int plume(GameObject Joueur)
    {
        Joueur.GetComponent<Jump>().ReduceFallSpeed();
        return 1;
    }

    public static int poulet(GameObject Joueur)
    {
        if (Joueur.GetComponent<CharacterThings>().HP == Joueur.GetComponent<CharacterThings>().MaxHP)
        {
            Joueur.GetComponent<CharacterThings>().MaxHP += 25;
        }
        else
        {
            Joueur.GetComponent<CharacterThings>().HP = Joueur.GetComponent<CharacterThings>().MaxHP;
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
        Joueur.GetComponent<CharacterThings>().cape = true;
        return 1;
    }

    public static int Infinity(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().Infini = true;
        return 1;
    }

    public static int Snipe(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().Snipe = true;
        return 1;
    }

    public static int Point(GameObject Joueur)
    {
        Joueur.GetComponentInChildren<Attack>().Damage += 40;
        return 1;
    }

    public static int Pyro(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().Pyro = true;
        return 1;
    }

    public static int Arcanes(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().Arcanes = true;
        return 1;
    }

}