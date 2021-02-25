using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Classes : MonoBehaviour
{


    public Sprite BottePeg;
    
    
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

    public static Dictionary<uint, Item> Common = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Rare = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Epic = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Relique = new Dictionary<uint, Item>();

    
    public static Dictionary<uint, Item>[] AllItem = {Common,Rare,Epic,Relique}; //0: common | 1: rare | 2: epic | 3: relique 
    
    void Awake()
    {
        AllItem[0].Add(0,Item.CreateItem("Botte de Pegaz", "À fonds les gaz, ça me botte !",CommonImage, BottePeg,Effect.Peg,  true,  false));
        AllItem[0].Add(1,Item.CreateItem("Baskets de Pegase", "Il parait que Pégase ne les a jamais misent lui-même, ça ne lui va pas au teint",CommonImage, BottePeg,Effect.Basket,  true,  false));
        AllItem[0].Add(2,Item.CreateItem("Oeil de Lynx", "L'oeil du lynx, la grâce du chamoix et la force de la loutre",CommonImage, BottePeg,Effect.Lynx,  true,  false));
        AllItem[0].Add(3,Item.CreateItem("Cecite", "Il vous vient une soudaine envie de jouer de la trompette",CommonImage, BottePeg,Effect.cécité,  true,  false));
        AllItem[0].Add(4,Item.CreateItem("Chaussures en savon", "Si je t'attrape toi...",CommonImage, BottePeg,Effect.savon,  true,  false));

        
        
        AllItem[1].Add(0,Item.CreateItem("Amour du Sang",   "Le sang, tu l'aimes ou tu le quittes",RareImage, BottePeg,Effect.BloodLove,  true,  false));
        AllItem[1].Add(1,Item.CreateItem("Trefle à quatre",   "Il finira probablement dans l'herbier de votre grand-mère",RareImage, BottePeg,Effect.trèfle,  true,  false));
        AllItem[1].Add(2,Item.CreateItem("Elixir de vie",  "Vous ne voulez pas savoir de quoi c'est fait, croyez-moi",RareImage, BottePeg, Effect.OneUp,true, false));


        AllItem[2].Add(0,Item.CreateItem("L'anneau unique",  "Attention à ne pas attirer le mauvais œil",EpicImage, BottePeg, Effect.theRing,  true,  false));
        AllItem[2].Add(1,Item.CreateItem("Dard","Omae wa mou shindeiru",EpicImage, BottePeg,Effect.Dard,  true,  false));

        
        AllItem[3].Add(0,Item.CreateItem("Ticket de loterie",  "Beaucoup d'espoir, peu de résultats",ReliqueImage, BottePeg, Effect.Ticket,true, false));
        AllItem[3].Add(1,Item.CreateItem("Instinct de Tueur",  "Tu permets que je te tue toi ?",ReliqueImage, BottePeg, Effect.KillerInstinct,true, false));

    }
    
}
