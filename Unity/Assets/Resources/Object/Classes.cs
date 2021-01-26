using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public class Item
    {
        protected float speed;
        protected float power;
        protected float pv;
        protected string name;
        protected string description;
        
        public Item CreateItem(string name, float speed, float power, float pv, string description, bool isPassive, bool isKnife, float range)
        {
            if (!isPassive)
            {
                if (isKnife)
                {
                    return new knife(name, speed, power, pv, description,range);
                }
                else
                {
                    return new gun(name, speed, power, pv, description,range);
                }
            }
            else
            {
                return new passive(name, speed, power, pv, description);
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
        public passive(string name, float speed, float power, float pv, string description)
        {
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
        }
    }
    public class knife : Item
    {
        public float range;
        public bool equiped;
        
        public knife(string name, float speed, float power, float pv, string description,float range)
        {
            this.range = range;
            this.equiped = true;
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
        }
    }

    public class gun : Item
    {
        public float range;
        public bool equiped;
        
        public gun(string name, float speed, float power, float pv, string description,float range)
        {
            this.range = range;
            this.equiped = true;
            this.name = name;
            this.speed = speed;
            this.power = power;
            this.pv = pv;
            this.description = description;
        }
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<uint, Item>[] AllItem = new Dictionary<uint, Item>[4]; //0: common | 1: rare | 2:epic | 3:relique 
        ///AllItem[0].Add(CreateItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
