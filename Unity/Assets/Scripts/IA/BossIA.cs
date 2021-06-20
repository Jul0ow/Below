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
            animator.SetBool("Die",true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen = true;
            GameObject.FindWithTag("Player").GetComponent<MovementSolo>().freeLook.GetComponent<CinemachineFreeLook>().enabled = false;
            GameObject.Find("écran de fin").transform.Find("Ecran victoire").gameObject.SetActive(true);
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
        
        else if (Life.Health > 8000)
        {
            Serie1.SetActive(false);
            if (playerInAttackRange)
            {
                animator.SetBool("Run forward",false);
                agent.SetDestination(transform.position);
                transform.LookAt(player.transform);
                if (!alreadyAttacked)
                {
                    Collider[] enemies = Physics.OverlapSphere(transform.position, 2);
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].CompareTag("Player"))
                        {
                            enemies[i].GetComponent<CharacterThings>().TakeDamage(0, false, false);
                            enemies[i].GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, 3);
                        }
                    }
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                else
                {
                    if (playerInSightRange)
                    {
                        animator.SetBool("Run forward",true);
                        Chaseplayer();
                    }
                    else
                    {
                        animator.SetBool("Stab Attack",true);
                        Attackplayer();
                    }
                }
            }
        }
        
        else if (Life.Health > 2000)
        {
            animator.SetBool("Run forward",false);
            animator.SetBool("Stab Attack",false);
            animator.SetBool("Cast spell",true);
            Summonner.SetActive(true);
            Collider[] blood = Physics.OverlapSphere(transform.position, 3);
            for (int i = 0; i < blood.Length; i++)
            {
                if (blood[i].gameObject.GetComponent<BloodIA>())
                {
                    Destroy(blood[i]);
                    Life.Health += 300;
                }
            }
            Attackplayer();
        }

        else
        {
            animator.SetBool("Cast spell",false);
            animator.SetBool("Jump",true);
            Summonner.SetActive(false);
            Serie2.SetActive(true);
            Light.SetActive(true);
            Lava.transform.position += new Vector3(0,0.0001f,0);
            if (Life.Health < 1000) Life.Health += 500;
            Attackplayer();
        }
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
