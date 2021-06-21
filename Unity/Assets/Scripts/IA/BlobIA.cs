using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class BlobIA : EnnemyIA
{
    public float lifes;
    private GameObject blob1;
    private GameObject blob2;

    protected override void Awake()
    {
        gameObject.transform.localScale = new Vector3(lifes, lifes, lifes);
        player = GameObject.FindGameObjectWithTag("Cible");
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected override void Update()
    {
        gameObject.transform.localScale = new Vector3(lifes/2, lifes/2, lifes/2);
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
            if (blockedPosition == transform.position)
            {
                walkpointSet = false;
            }
            blocked = false;
        }
        

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
            Collider[] enemies = Physics.OverlapSphere(transform.position, 6);
            for (int i = 0; i < enemies.Length; i++)
            {
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
        if(lifes >= 1)
        {
            if (solo)
            {
                blob1 = (GameObject) Instantiate(Resources.Load("PhotonPrefabs/Mob/Gout"), transform.position, Quaternion.identity);
                blob2 = (GameObject) Instantiate(Resources.Load("PhotonPrefabs/Mob/Gout"), transform.position, Quaternion.identity);
                blob1.GetComponent<BlobIA>().solo = true;
                blob2.GetComponent<BlobIA>().solo = true;
            }
            else
            {
                blob1 = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/Gout", transform.position, Quaternion.identity);
                blob2 = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/Gout", transform.position, Quaternion.identity);
            }
            blob1.GetComponent<BlobIA>().lifes = lifes-0.5f;
            blob2.GetComponent<BlobIA>().lifes = lifes-0.5f;
            if(solo)
                Destroy(gameObject);
            else
                PhotonNetwork.Destroy(gameObject);
        }
        if(solo)
            Destroy(gameObject);
        else
            PhotonNetwork.Destroy(gameObject);
    }
}
