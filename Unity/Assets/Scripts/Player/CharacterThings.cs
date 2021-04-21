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
    public int armor = 0;
    public bool theRing = false;
    public bool basketpeg = false;
    public bool runningInThe90s;
    public float basket = 3f;
    public bool OneUp = false;
    public int luck = 0;
    public bool vampire = false;
    public bool bloodLove;
    public (int, char) Room;
    private GameObject lifeBarObjetct;
    private LifeScript LifeBar;
    private bool invulnerable;
    private float tookDamage;
    private float invinciblityTime = 0.25f;
    
    
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
        if (!invulnerable)
        {
            if (bloodLove)
            {
                damage *= 2;
            }
            if (damage > armor)
            {
                HP -= damage-armor;
            }

            else
            {
                HP -= 1;
            }
            if(HP<=0) Destroy(gameObject);
            if (basketpeg && !runningInThe90s)
            {
                runningInThe90s = true;
                GetComponent<Movement>().WalkSpeed += 15;
                GetComponent<Movement>().RunSpeed += 15;
            }
            invulnerable = true;
            tookDamage = Time.time;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (rb.position.y < -10f)
        {
            LifeBar.HP = 0;
        }
        LifeBar.HP = HP;
        if (HP <= 0)
        {
            Time.timeScale = 0;
        }

        if (Time.time > tookDamage + invinciblityTime)
        {
            invulnerable = false;
        }

        if (runningInThe90s && Time.time > tookDamage + basket)
        {
            GetComponent<Movement>().WalkSpeed -= 15;
            GetComponent<Movement>().RunSpeed -= 15;
            runningInThe90s = false;
        }

    }
}
