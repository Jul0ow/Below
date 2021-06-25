using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    public GameObject LifeBarFab;
    public GameObject LifeBarObject;
    //public Vector3 LifeBarposition;
    public GameObject HealthRef;
    public int MaxHP = 100;
    public int HP;
    public bool poisoned = false;
    public int tox = 0;
    public bool Alive = true;
    public int armor = 0;
    public bool ring;
    public bool killer;
    public bool cape;
    public bool dard;
    public bool basketpeg;
    public bool runningInThe90s;
    public float basket = 3f;
    public bool OneUp;
    public int luck = 0;
    public bool vampire;
    public bool bloodLove;
    public bool toile;
    public bool purulence;
    public bool Souffrance;
    public bool ventricule;
    public bool klepto;
    public bool Pastille;
    public (int, char) Room;
    public GameObject lifeBarObjetct;
    public LifeScript LifeBar;
    public bool invulnerable;
    public float tookDamage;
    public PhotonView PV;
    public float invisibilityTime = 2f;
    public float invinciblityTime = 0.25f;
    public float ventriculeTime = 0;

    //for death
    public AudioSource hurt;
    
    public float deathTime;
    public GameObject DeathTimer;
    public float timeofDeath;
    public float lastTimeBeforeDeath;
    public float timeTofinalFight;
    public RoomManager.Team myTeam;
    public Vector3 myspawn;
    public float timeAtStartOfTheGame;
    private bool isInArena = false;
    
    
    public virtual void Awake()
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
        timeAtStartOfTheGame = Time.timeSinceLevelLoad;

    }

    [PunRPC]
    public virtual void Heal(int healing)
    {
        HP += healing;
    }
    
    [PunRPC]
    public virtual void TakeDamage(int damage, bool slowed = false, bool poison = false)
    {
        GetComponent<Movement>().slowed = slowed;
        if (slowed)
        {
            GetComponent<Movement>().slowedTime = Time.timeSinceLevelLoad;
        }
        if (!invulnerable)
        {
            hurt.Play();
            if (poison && !Pastille)
            {
                poisoned = true;
            }
            if (bloodLove)
            {
                damage *= 2;
            }

            if (cape)
            {
                transform.Find("Personnage").Find("SkinPerso").gameObject.SetActive(false);
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
            tookDamage = Time.timeSinceLevelLoad;
        }
    }


    [PunRPC]
    public virtual void EndGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
        GameObject.Find("écran de fin").transform.Find("Ecran victoire").gameObject.SetActive(true);
        //GetComponent<Movement>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {  
        float time = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad > tookDamage + invinciblityTime)
        {
            invulnerable = false;
        }
        if(!PV.IsMine) return;
        LifeBar = lifeBarObjetct.GetComponent<LifeScript>();
        if (HP > MaxHP) HP = MaxHP;
        if (poisoned)
        {
            poison();
            gameObject.transform.Find("flou artistique").gameObject.SetActive(true);
        }
        if (rb.position.y < -10f)
        {
            LifeBar.HP = 0;
        }
        LifeBar.HP = HP; 
        if(Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.LeftControl)) TakeDamage(99999);
        
        if (HP <= 0 && Alive)
        {
            if (timeAtStartOfTheGame + lastTimeBeforeDeath < time)
            {
                if (OneUp)
                {
                    OneUp = false;
                    GetComponent<PhotonView>().RPC("Heal",RpcTarget.All, MaxHP);
                }
                else
                {
                    //Alive = false;
                    GetComponent<PhotonView>().RPC("EndGame", RpcTarget.Others);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
                    GetComponent<Movement>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
                    GameObject.Find("écran de fin").transform.Find("Ecran défaite").gameObject.SetActive(true);
                    return;
                }
                
            }

            if (!(timeAtStartOfTheGame + lastTimeBeforeDeath < time))
            {
                //Time.timeScale = 0;
                timeofDeath = Time.timeSinceLevelLoad;
                Alive = false;
                if (myTeam == RoomManager.Team.Red)
                {
                    GetComponent<CharacterController>().enabled = false;
                    transform.position = new Vector3(-234.41f, 19.04f, -10.26f); //RED death zone
                    GetComponent<CharacterController>().enabled = true;
                }
                else
                {
                    GetComponent<CharacterController>().enabled = false;
                    transform.position = new Vector3(-234.41f, 19.04f, -55.66f);//blue death zone
                    GetComponent<CharacterController>().enabled = true;
                }
                //Debug.Log(player.transform.position);
            }
            
            
        }
        if (!Alive)
        {
            if (!isdead())
            {
                //Debug.Log("end death");
                GetComponent<CharacterController>().enabled = false;
                transform.position = myspawn;
                GetComponent<CharacterController>().enabled = true;
                deathTime *= 1.15f; //increase by 15% the death time
                Alive = true;
                HP = MaxHP;
                DeathTimer.GetComponent<TextMeshProUGUI>().text = "";
            }  
        }
        if (timeAtStartOfTheGame + timeTofinalFight<time && !isInArena)
        {
            if (myTeam == RoomManager.Team.Red)
            {
                isInArena = true;
                GetComponent<CharacterController>().enabled = false;
                transform.position = new Vector3(-21.85821f, 4.7f, -508.96f);//RED arene spawn
                GetComponent<CharacterController>().enabled = true;
            }
            else
            {
                isInArena = true;
                GetComponent<CharacterController>().enabled = false;
                transform.position = new Vector3(-11.06f, 1.43f, -533.3f); //blue arene spawn
                GetComponent<CharacterController>().enabled = true;
            }
        }
        if (cape && Time.timeSinceLevelLoad > tookDamage + invisibilityTime)
        {
            transform.Find("Personnage").Find("SkinPerso").gameObject.SetActive(true);
        }

        if (runningInThe90s && Time.timeSinceLevelLoad > tookDamage + basket)
        {
            GetComponent<Movement>().WalkSpeed -= 15;
            GetComponent<Movement>().RunSpeed -= 15;
            runningInThe90s = false;
        }
        
        if (ventricule && Time.timeSinceLevelLoad >= 3 + ventriculeTime)
        {
            Heal(1);
            ventriculeTime = Time.timeSinceLevelLoad;
        }

    }

    [PunRPC]
    protected virtual void poison()
    {
        if (Pastille)
        {
            return;
        }
        tox++;
        if(tox%100==0) TakeDamage(1, false, false);
        if (tox >= 20000)
        {
            poisoned = false;
            tox = 0;
            gameObject.transform.Find("flou artistique").gameObject.SetActive(false);
        }
    }

    public virtual bool isdead()
    {
        //Debug.Log("date : "+Time.time + " death Time: " + deathTime + " time of death :" + timeofDeath);
        DeathTimer.GetComponent<TextMeshProUGUI>().text = "Vous etes mort." + "\n" + "     " + Convert.ToString(Convert.ToInt32(deathTime + timeofDeath - Time.timeSinceLevelLoad));
        
        return Time.timeSinceLevelLoad < deathTime + timeofDeath;
    }
}
