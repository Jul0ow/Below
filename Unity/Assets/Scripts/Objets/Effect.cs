using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
            int ItemReference = Random.Range(0, Classes.AllItem[i].Count);
            Classes.Item content = Classes.AllItem[i][ItemReference];
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
        Joueur.GetComponentInChildren<PostProcessVolume>().enabled = true;
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
        Joueur.GetComponent<NewShoot>().BonusDamage += 50;
        return 1;
    }

    public static int Arcanes(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().Arcanes = true;
        return 1;
    }

    public static int Prot(GameObject Joueur)
    {
        Joueur.GetComponentInChildren<Attack>().Prot = true;
        return 1;
    }

    public static int Toile(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().toile = true;
        return 1;
    }

    public static int Polypheme(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().fireRate += 0.20f;
        Joueur.GetComponent<NewShoot>().BonusDamage += 75;
        return 1;
    }

    public static int Soja(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().fireRate -= 0.30f;
        Joueur.GetComponent<NewShoot>().BonusDamage -= 50;
        return 1;
    }

    public static int Purulence(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().purulence = true;
        return 1;
    }

    public static int BloodDrink(GameObject Joueur)
    {
        Joueur.GetComponent<NewShoot>().fireRate -= 0.30f;
        return 1;
    }

    public static int Folie(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().MaxHP -= 20;
        int ItemReference = Random.Range(0, Classes.AllItem[3].Count);
        Classes.Item content = Classes.AllItem[3][ItemReference];
        content.Joueur = Joueur;
        content.AppliedEffect();
        return 1;
    }

    public static int Souffrance(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().Souffrance = true;
        return 1;
    }

    public static int Ventricule(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().ventricule = true;
        return 1;
    }

    public static int Klepto(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().klepto = true;
        return 1;
    }

    public static int Pastille(GameObject Joueur)
    {
        Joueur.GetComponent<CharacterThings>().Pastille = true;
        return 1;
    }

}