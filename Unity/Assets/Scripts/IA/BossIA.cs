using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BossIA : EnnemyIA
{
    public int Force;
    public EnnemyLife Life;
    public GameObject Serie1;
    public GameObject Summonner;
    public GameObject Serie2;
    public GameObject Lava;
    public GameObject Light;
    public bool Dead;
    public Animator animator;


    protected override void Update()
    {
        if(Dead)
        {
            if (animator.speed > 0)
                animator.speed -= 0.01f;
            Collider[] getters = Physics.OverlapSphere(transform.position, 10);
            for (int i = 0; i < getters.Length; i++)
                if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
                    GameObject.FindWithTag("Player").GetComponent<MovementSolo>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
                    GameObject.Find("écran de fin").transform.Find("Ecran victoire").gameObject.SetActive(true);
                }
            return;
        }
        float distance = float.MaxValue;
        foreach (var joueur in GameObject.FindGameObjectsWithTag("Cible"))
        {
            float newDistance = Vector3.Distance(joueur.transform.position, this.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
            }
        }

        agent.speed = speed;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsplayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsplayer);

        if (Life.Health > 16000)
        {
            Attackplayer();
        }
        else if (Life.Health > 12000)
        {
            animator.SetBool("Run Forward",true);
            Serie1.SetActive(false);
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsplayer);
            /*if(!playerInSightRange)
                Attackplayer();
            else*/
                Chaseplayer();
            if (!alreadyAttacked)
            {
                Collider[] enemies = Physics.OverlapSphere(transform.position, 12);
                for (int i = 0; i < enemies.Length; i++)
                { 
                    if (enemies[i].CompareTag("Player"))
                    { 
                        animator.SetBool("Stab Attack", true);
                        enemies[i].GetComponent<CharacterThings>().TakeDamage(50, false, false);
                        enemies[i].GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, 12);
                    }
                }
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        
        else if (Life.Health > 5000)
        {
            animator.SetBool("Run Forward",false);
            animator.SetBool("Cast Spell",true);
            Summonner.SetActive(true);
            Collider[] blood = Physics.OverlapSphere(transform.position, 13);
            for (int i = 0; i < blood.Length; i++)
            {
                if (blood[i].gameObject.GetComponent<BloodIA>())
                {
                    Destroy(blood[i].gameObject);
                    Life.Health += 500;
                }
            }
            Attackplayer();
        }

        else
        {
            animator.SetBool("Cast Spell",false);
            animator.SetBool("Jump",true);
            Summonner.SetActive(false);
            Serie2.SetActive(true);
            Light.SetActive(true);
            Lava.transform.position += new Vector3(0,0.0075f,0);
            if(Lava.transform.position.y >= 35) player.GetComponent<CharacterThings>().TakeDamage(99999);
            if (Life.Health < 4000) Life.Health += 1000;
            Attackplayer();
        }
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
