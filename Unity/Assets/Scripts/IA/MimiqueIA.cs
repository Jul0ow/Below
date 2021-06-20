using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MimiqueIA : EnnemyIA
{
    public Classes.Item content;
    public GameObject Getter;
    public int Rarity;
    public int ItemReference;
    
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
        Chaseplayer();
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsplayer);
        if(playerInAttackRange) Attackplayer();
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
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
                    enemies[i].GetComponent<CharacterThings>().TakeDamage(2, false, false);
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    public void Deathrattle()
    {
        if(content != null)
        {
            content.AppliedEffect();
            Getter.GetComponent<CharacterThings>().Inventory.Add(content);
            HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
        }
        if(solo)
            Destroy(gameObject);
        else
            PhotonNetwork.Destroy(gameObject);
    }
}
