using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CharacterThings : MonoBehaviour
{

    public Rigidbody rb;
    public List<Classes.Item> Inventory;
    private GameObject LifeBarFab;
    private GameObject LifeBarObject;
    //public Vector3 LifeBarposition;
    private GameObject HealthRef;
    public int MaxHP = 100;
    public int HP;
    public bool Alive = true;
    public int armor = 100;
    public bool theRing = false;
    public bool basketpeg = false;
    public bool OneUp = false;
    public int luck = 0;
    public bool savon = false;
    public (int, char) Room;
    private GameObject lifeBarObjetct;
    private LifeScript LifeBar;
    private bool invulnerable;
    private float tookDamage;
    private float invinviblityTime = 0.25f;
    public PhotonView PV;
    
    
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if(!PV.IsMine) return;
        LifeBarFab = GameObject.Find("Health");
        HealthRef = GameObject.Find("HealthRef");
        lifeBarObjetct = Instantiate(LifeBarFab, HealthRef.transform.position, Quaternion.identity, 
            GameObject.FindGameObjectWithTag("Canvas").transform);
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        LifeBar.SetMaxHealth(HP);
        LifeBar.MaxHP = MaxHP;
        LifeBar.HP = HP;
        Inventory = new List<Classes.Item>();
        
    }
    
    public void TakeDamage(int damage)
    {
        if(!PV.IsMine) return;
        if (!invulnerable)
        {
            HP -= damage;
            Debug.Log(HP);
            //if(HP<=0) Destroy(gameObject);
            invulnerable = true;
            tookDamage = Time.time;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine) return;
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (Input.GetKeyDown("k")) HP -= 10;
        if (rb.position.y < -10f)
        {
            LifeBar.HP = 0;
        }
        LifeBar.HP = HP;
        if (HP <= 0)
        {
            Time.timeScale = 0;
        }

        if (Time.time > tookDamage + invinviblityTime)
        {
            invulnerable = false;
        }
        
    }
}
