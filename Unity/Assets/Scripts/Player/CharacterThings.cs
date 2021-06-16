using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
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
    private int tox = 0;
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
    public bool invulnerable;
    private float tookDamage;
    public PhotonView PV;
    private float invisibilityTime = 2f;
    private float invinciblityTime = 0.25f;
    //for death
    public AudioSource hurt;
    
    public float deathTime;
    public GameObject DeathTimer;
    public float timeofDeath;
    private RoomManager.Team myTeam;
    private Vector3 myspawn;
    
    
    
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        myspawn= GetComponent<Transform>().position;
        if (myspawn.x == -13.10f && myspawn.y == 0.16f && myspawn.z ==-1027f) //set the team by looking of the first spawn position
        {
            myTeam = RoomManager.Team.Blue;
        }
        else
        {
            myTeam = RoomManager.Team.Red;
        }
        if(!PV.IsMine) return;
        LifeBarFab = GameObject.Find("Health");
        HealthRef = GameObject.Find("HealthRef");
        lifeBarObjetct = Instantiate(LifeBarFab, HealthRef.transform.position, Quaternion.identity, 
            GameObject.FindGameObjectWithTag("Canvas").transform);
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        LifeBar.SetMaxHealth(HP);
        LifeBar.MaxHP = MaxHP;
        LifeBar.HP = HP;
        DeathTimer = Instantiate(DeathTimer, new Vector3(Screen.width *0.6f, Screen.height *0.5f, 0),
            Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
        DeathTimer.GetComponent<TextMeshProUGUI>().text = "";
        Inventory = new List<Classes.Item>();

    }
    
    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            hurt.Play();
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
            //if(HP<=0) Destroy(gameObject);
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
        if (Time.time > tookDamage + invinciblityTime)
        {
            invulnerable = false;
        }
        if(!PV.IsMine) return;
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (poisoned) poison();
        if (rb.position.y < -10f)
        {
            LifeBar.HP = 0;
        }
        LifeBar.HP = HP; 
        if(Input.GetKey("k")) TakeDamage(99999);
       if (!Alive)
        {
            if (!isdead())
            {
                //Debug.Log("end death");
                transform.position = myspawn;
                deathTime *= 1.15f; //increase by 15% the death time
                Alive = true;
                HP = MaxHP;
                DeathTimer.GetComponent<TextMeshProUGUI>().text = "";
            }  
        }
        if (HP <= 0 && Alive)
        {
            //Time.timeScale = 0;
            timeofDeath = Time.time;
            Alive = false;
            if (myTeam == RoomManager.Team.Red)
                transform.position = new Vector3(-234.41f, 19.04f, -10.26f);//RED death zone
            else
            {
                transform.position = new Vector3(-234.41f, 19.04f, -55.66f);//blue death zone
            }
            //Debug.Log(player.transform.position);
            
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

    [PunRPC]
    private void poison()
    {
        tox++;
        if(tox%100==0) TakeDamage(1);
        if (tox >= 5000)
        {
            poisoned = false;
            tox = 0;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Lava")
            TakeDamage(100);
    }

    public bool isdead()
    {
        //Debug.Log("date : "+Time.time + " death Time: " + deathTime + " time of death :" + timeofDeath);
        DeathTimer.GetComponent<TextMeshProUGUI>().text = "Vous etes mort." + "\n" + "     " + Convert.ToString(Convert.ToInt32(deathTime + timeofDeath - Time.time));
        
        return Time.time < deathTime + timeofDeath;
    }
}
