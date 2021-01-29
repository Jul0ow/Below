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
        AllItem[0].Add(1,Item.CreateItem("Botte de Pégaz", "À fonds les gaz, ça me botte !", BottePeg,Effect.Peg,  true,  false));
        AllItem[1].Add(1,Item.CreateItem("Botte de Pégaz",   "Ça me botte !", BottePeg,Effect.Peg,  true,  false));
        AllItem[2].Add(1,Item.CreateItem("Botte de Pégaz",  "Ça me botte !", BottePeg,Effect.Peg,  true,  false));
        AllItem[3].Add(1,Item.CreateItem("Botte de Pégaz",  "Ça me botte !", BottePeg, Effect.Peg,true, false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
