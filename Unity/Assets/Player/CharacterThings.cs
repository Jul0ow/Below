using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThings : MonoBehaviour
{
    public Rigidbody rb;
    public List<Classes.Item> Inventory;
    public GameObject LifeBarFab;
    public Vector3 LifeBarposition;
    public int MaxHP = 100;
    public int HP;
    public bool Alive = true;
    
    
    void Start()
    {
        Inventory = new List<Classes.Item>();
        GameObject lifeBarObjetct = Instantiate(LifeBarFab,LifeBarposition,Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
        LifeScript LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        LifeBar.MaxHP = MaxHP;
        LifeBar.HP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
