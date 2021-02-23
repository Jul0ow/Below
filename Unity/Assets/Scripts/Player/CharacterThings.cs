using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterThings : MonoBehaviour
{
    public Rigidbody rb;
    public List<Classes.Item> Inventory;
    private GameObject LifeBarFab;
    private GameObject LifeBarObject;
    public Vector3 LifeBarposition;
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
    
    
    void Awake()
    {
        LifeBarFab = GameObject.Find("Health");
        lifeBarObjetct = Instantiate(LifeBarFab,LifeBarposition,Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        Inventory = new List<Classes.Item>();
        LifeBar.SetMaxHealth(HP);
        LifeBar.MaxHP = MaxHP;
        LifeBar.HP = HP;
    }
    
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
        if(HP<=0) Destroy(gameObject);
    } 

    // Update is called once per frame
    void Update()
    {
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (Input.GetKeyDown("k")) HP -= 10;
        if (rb.position.y < -10f)
        {
            LifeBar.HP = 0;
        }
        LifeBar.HP = HP;
        if (HP <= 0) Application.Quit();
    }
}
