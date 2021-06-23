using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MamaBouquetin : BouquetinIA
{
    private bool invoqued = false;
    public EnnemyLife Life;
    public GameObject Skull;
    
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
        if (Life.Health <= 500 && !invoqued)
        {
            invoqued = true;
            Skull.SetActive(false);
            if(solo)
            {
                Object crane;
                crane = Instantiate(Resources.Load("PhotonPrefabs/Mob/" + "Skull"), transform.position, Quaternion.identity);
                ((GameObject) crane).GetComponent<EnnemyIA>().solo = true;
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + "Skull", transform.position, Quaternion.identity);
            }
                
        }
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
