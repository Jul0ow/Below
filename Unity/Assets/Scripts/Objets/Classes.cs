using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Classes : MonoBehaviour
{


    /*              Gallerie de sprite                         */
    public Sprite BottePeg;
    public Sprite bloodLove;
    public Sprite theRing;
    public Sprite basketPeg;
    public Sprite Dard;
    public Sprite ticket;
    public Sprite killer;
    public Sprite blindness;
    public Sprite lynx;
    public Sprite elixir;
    public Sprite trefle;
    public Sprite savon;
    public Sprite plume;
    public Sprite Bouclier;
    public Sprite Cape;
    public Sprite Maille;
    public Sprite Poulet;
    public Sprite Vampire;
    public Sprite pilule;
    public Sprite snipe;
    public Sprite LaitSoj;
    public Sprite Pyro;
    public Sprite Arcanes;
    public Sprite poing;
    public Sprite Infi;
    public Sprite Klepto;
    public Sprite bloodDrink;
    public Sprite ventricule;
    public Sprite souffrance;
    public Sprite Toile;
    public Sprite Folie;
    public Sprite Purulence;
    public Sprite Poly;
    public Sprite prot;



    /*                  Sprite Rareté                          */
    public Sprite CommonImage;
    public Sprite RareImage;
    public Sprite EpicImage;
    public Sprite ReliqueImage;
    
    
    public abstract class Item
    {
        protected string Name;
        protected string Description;
        protected Sprite Rarity;
        public Sprite Sprite;
        public Func<GameObject,int> Effect;
        public GameObject Joueur;
        

        public static Item CreateItem(string name,string description, Sprite rarity, Sprite sprite, Func<GameObject,int> effect, bool isPassive, bool isKnife)
        {
            if (!isPassive)
            {
                if (isKnife)
                {
                    return new knife(name, description,rarity,sprite,effect);
                }
                return new gun(name,description,rarity,sprite,effect);
            }
            return new passive(name, description,rarity,sprite,effect);
        }
        public string Getname()
        {
            return Name;
        }
        public string Getdescription()
        {
            return Description;
        }
        
        public Sprite Getsprite()
        {
            return Sprite;
        }

        public Sprite GetRarity()
        {
            return Rarity;
        }

        public void AppliedEffect()
        {
            Effect(Joueur);
        }
    }

    public class passive : Item
    {
        public passive(string name, string description,Sprite rarity, Sprite sprite, Func<GameObject,int> effect)
        {
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
            Rarity = rarity;
        }
    }
    public class knife : Item
    {
        public float Range;
        public bool Equiped;
        public knife(string name, string description,Sprite rarity, Sprite sprite,  Func<GameObject,int> effect)
        {
            Equiped = true;
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
            Rarity = rarity;
        }
    }

    public class gun : Item
    {
        public bool Equiped;
        
        public gun(string name, string description,Sprite rarity, Sprite sprite,Func<GameObject,int> effect)
        {
            Equiped = true;
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
            Rarity = rarity;
        }
        
    }

    public static Dictionary<int, Item> Common = new Dictionary<int, Item>();
    public static Dictionary<int, Item> Rare = new Dictionary<int, Item>();
    public static Dictionary<int, Item> Epic = new Dictionary<int, Item>();
    public static Dictionary<int, Item> Relique = new Dictionary<int, Item>();

    
    public static Dictionary<int, Item>[] AllItem = {Common,Rare,Epic,Relique}; //0: common | 1: rare | 2: epic | 3: relique 
    
    void Awake()
    {
        AllItem[0].Add(0,Item.CreateItem("Botte de Pegaz", "À fonds les gaz, ça me botte !",CommonImage, BottePeg,Effect.Peg,  true,  false));
        AllItem[0].Add(1,Item.CreateItem("Baskets de Pegase", "Il parait que Pégase ne les à jamais misent lui-même, ça ne lui va pas au teint",CommonImage, basketPeg,Effect.Basket,  true,  false));
        AllItem[0].Add(2,Item.CreateItem("Oeil de Lynx", "L'oeil du lynx, la grâce du chamoix et la force de la loutre",CommonImage, lynx,Effect.Lynx,  true,  false));
        AllItem[0].Add(3,Item.CreateItem("Cécite", "Il vous vient une soudaine envie de jouer de la trompette",CommonImage, blindness,Effect.cécité,  true,  false));
        AllItem[0].Add(4,Item.CreateItem("Chaussures en savon", "Si je t'attrape toi...",CommonImage, savon,Effect.savon,  true,  false));
        AllItem[0].Add(5,Item.CreateItem("Poulet rôti", "Saura séduire chauves-souris, gorgones, momies et vampires",CommonImage, Poulet, Effect.poulet,true,  false));
        AllItem[0].Add(6,Item.CreateItem("Plume de vent",  "Vous vouliez une plume de Phœnix ? Dommage",CommonImage, plume, Effect.plume,true, false));
        AllItem[0].Add(7,Item.CreateItem("Infinité","Il n'y a que de choses infinies, l'univers et mes balles !", CommonImage, Infi,Effect.Infinity, true, false));
        AllItem[0].Add(8,Item.CreateItem("Gant de puissance", "Pour faire des ricochets et claquer des doigts", CommonImage, poing, Effect.Point, true, false));
        AllItem[0].Add(9,Item.CreateItem("Protéine", "Ah oui ?", CommonImage, prot, Effect.Prot, true, false));
        AllItem[0].Add(10,Item.CreateItem("Toile d'araignées", "Très utile, en soie", Toile, CommonImage, Effect.Toile, true, false));
        AllItem[0].Add(11,Item.CreateItem("Lait de Soja", "On a pas dit que c'était bon !", CommonImage, LaitSoj, Effect.Soja, true, false));
        AllItem[0].Add(12,Item.CreateItem("Souffrance délicieuse", "Y'a que ça de vrai", CommonImage, souffrance, Effect.Souffrance, true, false));
        AllItem[0].Add(13, Item.CreateItem("Ventricule", "Mettez-y du coeur", CommonImage,ventricule, Effect.Ventricule, true, false));
        AllItem[0].Add(14, Item.CreateItem("Kleptomanie", "Sa place est dans un musée !", CommonImage, Klepto, Effect.Klepto, true, false));

        AllItem[1].Add(0,Item.CreateItem("Amour du Sang",   "Le sang, tu l'aimes ou tu le quittes",RareImage, bloodLove,Effect.BloodLove,  true,  false));
        AllItem[1].Add(1,Item.CreateItem("Trèfle à quatre feuille",   "Il finira probablement dans l'herbier de votre grand-mère",RareImage, trefle,Effect.trèfle,  true,  false));
        AllItem[1].Add(2,Item.CreateItem("Elixir de vie",  "Vous ne voulez pas savoir de quoi c'est fait, croyez-moi",RareImage, elixir, Effect.OneUp,true, false));
        AllItem[1].Add(3,Item.CreateItem("Maille", "Il n'y a que la maille qui m'aille",RareImage,Maille, Effect.Maille, true, false));
        AllItem[1].Add(4,Item.CreateItem("Vampirisme", "Il ne vous manque plus que les poulets rôtis", RareImage,Vampire, Effect.Vampirisme,true, false));
        AllItem[1].Add(5,Item.CreateItem("Lunette de visé", "C'est l'effet boule de neige !", RareImage, snipe, Effect.Snipe, true, false));
        AllItem[1].Add(6,Item.CreateItem("Polypheme", "Attention à ne pas attirer le troisième oeil", RareImage, Poly, Effect.Polypheme, true, false));
        AllItem[1].Add(7,Item.CreateItem("Purulence", "La lèpre, indémodable", RareImage, Purulence, Effect.Purulence, true, false));
        AllItem[1].Add(8,Item.CreateItem("Sombrer dans la folie","Enfin...", RareImage,Folie, Effect.Folie, true, false));
        AllItem[1].Add(9, Item.CreateItem("Pastille anti-douleur", "Déposez-la sur votre langue et fermez les yeux", RareImage, pilule, Effect.Pastille, true, false));
        

        AllItem[2].Add(0,Item.CreateItem("L'anneau unique",  "Attention à ne pas attirer le mauvais œil",EpicImage, theRing, Effect.theRing,  true,  false));
        AllItem[2].Add(1,Item.CreateItem("Dard","Il ne le sait pas encore mais il est déjà mort",EpicImage, Dard,Effect.Dard,  true,  false));
        AllItem[2].Add(2,Item.CreateItem("Dernier rempart", "la meilleure défense c'est la défense", EpicImage, Bouclier, Effect.Bouclier, true, false));
        AllItem[2].Add(3,Item.CreateItem("Cape", "Tu es invisible Harry",EpicImage, Cape, Effect.Cape, true, false));
        AllItem[2].Add(4,Item.CreateItem("Pyromancie", "A tenir hors de portée des enfants",EpicImage, Pyro, Effect.Pyro, true ,false));
        AllItem[2].Add(5,Item.CreateItem("Verre de sang", "Le soja n'était pas si mal finalement", EpicImage, bloodDrink, Effect.BloodDrink, true, false));
        
        AllItem[3].Add(0,Item.CreateItem("Ticket de loterie",  "Beaucoup d'espoir, peu de résultats",ReliqueImage, ticket, Effect.Ticket,true, false));
        AllItem[3].Add(1,Item.CreateItem("Instinct de Tueur",  "Tu permets que je te tue toi ?",ReliqueImage, killer, Effect.KillerInstinct,true, false));
        AllItem[3].Add(2,Item.CreateItem("Maitrise des Arcanes", "Si seulement je savais lire...", ReliqueImage, Arcanes, Effect.Arcanes, true, false));
        

    }
    
}
