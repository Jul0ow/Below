using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public bool poisoned = false;
    private bool tox = false;
    public bool Alive = true;
    public int armor = 0;
    public bool ring;
    public bool killer;
    public bool cape = false;
    public bool dard = false;
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
    public PhotonView PV;
    private float invisibilityTime = 2f;
    private float invinciblityTime = 0.25f;
    
    
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
            if (bloodLove)
            {
                damage *= 2;
            }

            if (cape)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
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
        if(!PV.IsMine) return;
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (poisoned) poison();
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

        if (cape && Time.time > tookDamage + invisibilityTime)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }

        if (runningInThe90s && Time.time > tookDamage + basket)
        {
            GetComponent<Movement>().WalkSpeed -= 15;
            GetComponent<Movement>().RunSpeed -= 15;
            runningInThe90s = false;
        }

    }

    private void poison()
    {
        poisoned = false;
        tox = true;
        while (tox)
        {
            StartCoroutine(waitpoison());
        }
    }

    private IEnumerator waitpoison()
    {
        TakeDamage(5);
        int rnd = Random.Range(0, 6);
        if (rnd == 2) tox = false;
        yield return new WaitForSeconds(1);
    }
}
