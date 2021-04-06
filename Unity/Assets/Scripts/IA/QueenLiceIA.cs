using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class QueenLiceIA : EnnemyIA
{
    public GameObject splitter;

    
    protected override void Attackplayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            for (int i = 0; i < 5; i++)
                PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + splitter.name, transform.position, Quaternion.identity);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
}
