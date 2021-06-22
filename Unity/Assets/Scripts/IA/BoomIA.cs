using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class BoomIA : RushIA
{
    public GameObject explosion;
    
    protected override void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Cible");
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected override void Update()
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

    protected override void SearchWalkpoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
            walkpointSet = true;
    }

    protected override void Patroling()
    {
        if (!walkpointSet) SearchWalkpoint();
        if(walkpointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkpoint = transform.position - walkpoint;
        if (distanceToWalkpoint.magnitude < 1f)
            walkpointSet = false;
    }

    protected override void Chaseplayer()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }

    protected override void Attackplayer()
    {
        if(!alreadyAttacked)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, 2);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Player"))
                {
                    Deathrattle();
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Deathrattle()
    {
        GameObject currentexplosion;
        currentexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(currentexplosion.transform.position, 5);
        for (int i = 0; i < enemies.Length; i++)
        {
            // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
            if (enemies[i].CompareTag("Player"))
            {
                if (solo)
                {
                    enemies[i].GetComponent<CharacterThings>().TakeDamage(damage, false, false);
                }
                else
                {
                    enemies[i].GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, false, false);
                }
            }
            if(solo)
                Destroy(gameObject);
            else if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
                
        }
    }
}
