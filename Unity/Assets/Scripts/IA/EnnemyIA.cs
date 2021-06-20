using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnnemyIA : MonoBehaviour
{
    public bool IsElite = false;
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsplayer;
    public Vector3 walkpoint;
    public bool walkpointSet;
    public float walkpointrange;
    public float timeBetweenAttacks;
    public float speed;
    public int damage;
    public bool alreadyAttacked = false;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public (int,char) Room;
    public EnnemyAttack attack;
    public float blockedTime;
    public Vector3 blockedPosition;
    public bool blocked = false;
    public bool solo = false;


    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Cible");
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
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
        agent.speed = speed;
        playerInSightRange = !player.GetComponentInParent<CharacterThings>().ring && Physics.CheckSphere(transform.position, sightRange, whatIsplayer);
        playerInAttackRange = !player.GetComponentInParent<CharacterThings>().ring && Physics.CheckSphere(transform.position, attackRange, whatIsplayer);
        if(playerInSightRange && !playerInAttackRange) Chaseplayer();
        else if(playerInAttackRange && playerInSightRange) Attackplayer();
        else Patroling();
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }


    protected virtual void Patroling()
    {
        if (!walkpointSet) SearchWalkpoint();
        if (walkpointSet)
        {
            agent.SetDestination(walkpoint);
            if (!blocked)
            {
                blocked = true;
                blockedTime = Time.time;
                blockedPosition = transform.position;
            }
            
        }

        if (blocked && blockedTime+3 <= Time.time)
        {
            if (transform.position.x <= blockedPosition.x+1 && transform.position.x >= blockedPosition.x-1 && transform.position.y <= blockedPosition.y+1 && transform.position.y >= blockedPosition.y-1 && transform.position.z <= blockedPosition.z+1 && transform.position.z >= blockedPosition.z-1)
            {
                walkpointSet = false;
            }
            blocked = false;
        }
        

        Vector3 distanceToWalkpoint = transform.position - walkpoint;
        if (distanceToWalkpoint.magnitude < 1f)
            walkpointSet = false;
    }

    protected virtual void SearchWalkpoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkpointSet = true;
    }

    protected virtual void Chaseplayer()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }

    protected virtual void Attackplayer()
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

    protected virtual void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    
}
