using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public Sprite BottePeg;
    public abstract class Item
    {
        protected string Name;
        protected string Description;
        public Sprite Sprite;
        public Func<GameObject,int> Effect;
        public GameObject Joueur;

        public static Item CreateItem(string name,string description,Sprite sprite, Func<GameObject,int> effect, bool isPassive, bool isKnife)
        {
            if (!isPassive)
            {
                if (isKnife)
                {
                    return new knife(name, description,sprite,effect);
                }
                return new gun(name,description,sprite,effect);
            }
            return new passive(name, description,sprite,effect);
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

        public void AppliedEffect()
        {
            Effect(Joueur);
        }
    }

    public class passive : Item
    {
        public passive(string name, string description,Sprite sprite, Func<GameObject,int> effect)
        {
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
        }
    }
    public class knife : Item
    {
        public float Range;
        public bool Equiped;
        public knife(string name, string description, Sprite sprite,  Func<GameObject,int> effect)
        {
            Equiped = true;
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
        }
    }

    public class gun : Item
    {
        public bool Equiped;
        
        public gun(string name, string description, Sprite sprite,Func<GameObject,int> effect)
        {
            Equiped = true;
            Name = name;
            Description = description;
            Sprite = sprite;
            Effect = effect;
        }
        
    }

    public static Dictionary<uint, Item> Common = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Rare = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Epic = new Dictionary<uint, Item>();
    public static Dictionary<uint, Item> Relique = new Dictionary<uint, Item>();

    
    public static Dictionary<uint, Item>[] AllItem = {Common,Rare,Epic,Relique}; //0: common | 1: rare | 2:epic | 3:relique 

    
    // Start is called before the first frame update
    void Awake()
    {
        AllItem[0].Add(0,Item.CreateItem("Botte de Pégaz", "À fonds les gaz, ça me botte !", BottePeg,Effect.Peg,  true,  false));
        AllItem[0].Add(1,Item.CreateItem("Basket de Pégase", "Il parait que Pégase ne les a jamais misent lui-même, ça ne lui va pas au teint", BottePeg,Effect.Basket,  true,  false));
        AllItem[0].Add(2,Item.CreateItem("Oeil de Lynx", "L'oeil du lynx, la grâce du chamoix et la force de la loutre", BottePeg,Effect.Lynx,  true,  false));
        AllItem[0].Add(3,Item.CreateItem("Cécité", "Il vous vient une soudaine envie de jouer de la trompette", BottePeg,Effect.cécité,  true,  false));
        AllItem[0].Add(4,Item.CreateItem("Chaussure en savon", "Si je t'attrape toi...", BottePeg,Effect.savon,  true,  false));

        
        
        AllItem[1].Add(0,Item.CreateItem("Amour du Sang",   "Le sang, tu l'aimes ou tu le quittes", BottePeg,Effect.BloodLove,  true,  false));
        AllItem[1].Add(1,Item.CreateItem("Trèfle à quatre",   "Il finira probablement dans l'herbier de votre grand-mère", BottePeg,Effect.trèfle,  true,  false));


        AllItem[2].Add(0,Item.CreateItem("L'anneau unique",  "Attention à ne pas attirer le mauvais œil", BottePeg, Effect.theRing,  true,  false));
        AllItem[2].Add(1,Item.CreateItem("Dard","Omae wa mou shindeiru", BottePeg,Effect.Dard,  true,  false));

        
        AllItem[3].Add(0,Item.CreateItem("Ticket de loterie",  "Beaucoup d'espoir, peu de résultats", BottePeg, Effect.Ticket,true, false));
        AllItem[3].Add(1,Item.CreateItem("Instinct de Tueur",  "Tu permets que je te tue toi ?", BottePeg, Effect.KillerInstinct,true, false));
        AllItem[3].Add(2,Item.CreateItem("Elixir de vie",  "Vous ne voulez pas savoir de quoi c'est fait, croyez-moi", BottePeg, Effect.OneUp,true, false));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
