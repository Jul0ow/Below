using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public Sprite BottePeg;
    
    public class Item
    {
        protected float speed;
        protected float power;
        protected float pv;
        protected string name;
        protected string description;
        public Sprite sprite;
        
        public static Item CreateItem(string name, float speed, float power, float pv, string description,Sprite sprite, bool isPassive, bool isKnife, float range)
        {
            if (!isPassive)
            {
                if (isKnife)
                {
                    return new knife(name, speed, power, pv, description,sprite,range);
                }
                else
                {
                    return new gun(name, speed, power, pv, description,sprite,range);
                }
            }
            else
            {
                return new passive(name, speed, power, pv, description,sprite);
            }
        }
        public string Getname()
        {
            return name;
        }
        public float Getpower()
        {
            return power;
        }
        public float Getpv()
        {
            return pv;
        }
        public string Getdescription()
        {
            return description;
        }
        public float Getspeed()
        {
            return speed;
        }
    }

    public class passive : Item
    {
        public passive(string name, float speed, float power, float pv, string description,Sprite sprite)
        {
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
            this.sprite = sprite;
        }
    }
    public class knife : Item
    {
        public float range;
        public bool equiped;
        
        public knife(string name, float speed, float power, float pv, string description, Sprite sprite, float range)
        {
            this.range = range;
            this.equiped = true;
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
            this.sprite = sprite;
        }
    }

    public class gun : Item
    {
        public float range;
        public bool equiped;
        
        public gun(string name, float speed, float power, float pv, string description, Sprite sprite,float range)
        {
            this.range = range;
            equiped = true;
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
            this.sprite = sprite;
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
        AllItem[0].Add(1,Item.CreateItem("Botte de Pégaz", 10f,  0f,  0f,  "À fonds les gaz, ça me botte !", BottePeg,  true,  false,  0f));
        AllItem[1].Add(1,Item.CreateItem("Botte de Pégaz", 10f,  0f,  0f,  "Ça me botte !", BottePeg,  true,  false,  0f));
        AllItem[2].Add(1,Item.CreateItem("Botte de Pégaz", 10f,  0f,  0f,  "Ça me botte !", BottePeg,  true,  false,  0f));
        AllItem[3].Add(1,Item.CreateItem("Botte de Pégaz", 10f, 0f, 0f, "Ça me botte !", BottePeg, true, false, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
