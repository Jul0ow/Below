using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnnemyIA : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsplayer;
    public Vector3 walkpoint;
    private bool walkpointSet;
    public float walkpointrange;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public int Health;
    public (int,char) Room;
    public EnnemyAttack attack;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Cible");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = float.MaxValue;
        foreach (var joueur in GameObject.FindGameObjectsWithTag("Cible"))
        {
            float newDistance = Vector3.Distance(joueur.transform.position, this.transform.position);
            if (newDistance<distance)
            {
                distance = newDistance;
                player = joueur;
            }
        }
        agent.speed = 3.5f;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsplayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsplayer);
        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) Chaseplayer();
        if(playerInAttackRange && playerInSightRange) Attackplayer();
    }
    
    

    private void Patroling()
    {
        if (!walkpointSet) SearchWalkpoint();
        if(walkpointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkpoint = transform.position - walkpoint;
        if (distanceToWalkpoint.magnitude < 1f)
            walkpointSet = false;
    }

    private void SearchWalkpoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
            walkpointSet = true;
    }
    
    private void Chaseplayer()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }
    
    private void Attackplayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            attack.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
         {
             Health -= damage;
             if(Health<=0) Destroy(gameObject);
         } 

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
