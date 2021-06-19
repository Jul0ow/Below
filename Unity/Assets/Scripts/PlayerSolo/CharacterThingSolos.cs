using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterThingSolos : CharacterThings
{


    public override void Awake()
    {
        
        myspawn= GetComponent<Transform>().position;
        if (myspawn.x == -13.10f && myspawn.y == 0.16f && myspawn.z ==-1027f) //set the team by looking of the first spawn position
        {
            myTeam = RoomManager.Team.Blue;
        }
        else
        {
            myTeam = RoomManager.Team.Red;
        }
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
        timeAtStartOfTheGame = Time.time;

    }

    
    public override void Heal(int healing)
    {
        HP += healing;
    }
    
    
    public override void TakeDamage(int damage, bool slowed = false, bool poison = false)
    {
        GetComponent<Movement>().slowed = slowed;
        if (slowed)
        {
            GetComponent<Movement>().slowedTime = Time.time;
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


    
    public override void EndGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
        GameObject.Find("écran de fin").transform.Find("Ecran victoire").gameObject.SetActive(true);
        //GetComponent<Movement>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {  
        float time = Time.time;
        if (Time.time > tookDamage + invinciblityTime)
        {
            invulnerable = false;
        }
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
            if (timeAtStartOfTheGame + lastTimeBeforeDeath< time)
            {
                Alive = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
                GetComponent<MovementSolo>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
                GameObject.Find("écran de fin").transform.Find("Ecran défaite").gameObject.SetActive(true);
                return;
            }
            
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
        if (timeAtStartOfTheGame + timeTofinalFight<time)
        {
            if (myTeam == RoomManager.Team.Red)
                transform.position = new Vector3(-19.3f, 1.43f, -507.28f);//RED arene spawn
            else
            {
                transform.position = new Vector3(-11.06f, 1.43f, -533.3f);//blue arene spawn
            }
        }
        if (cape && Time.time > tookDamage + invisibilityTime)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }

        if (runningInThe90s && Time.time > tookDamage + basket)
        {
            GetComponent<MovementSolo>().WalkSpeed -= 15;
            GetComponent<MovementSolo>().RunSpeed -= 15;
            runningInThe90s = false;
        }
        
        if (ventricule)
        {
            HP += 1;
        }

    }

    
    protected override void poison()
    {
        if (Pastille)
        {
            return;
        }
        tox++;
        if(tox%100==0) TakeDamage(1, false, false);
        if (tox >= 5000)
        {
            poisoned = false;
            tox = 0;
            gameObject.transform.Find("flou artistique").gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Lava")
            TakeDamage(100, false, false);
    }

    public override bool isdead()
    {
        //Debug.Log("date : "+Time.time + " death Time: " + deathTime + " time of death :" + timeofDeath);
        DeathTimer.GetComponent<TextMeshProUGUI>().text = "Vous etes mort." + "\n" + "     " + Convert.ToString(Convert.ToInt32(deathTime + timeofDeath - Time.time));
        
        return Time.time < deathTime + timeofDeath;
    }
}
