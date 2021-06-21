using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BouquetinIA : EnnemyIA
{
    public Animator _animator;
    private int timeBetweenAttacksCAC = 3;
    protected override void Chaseplayer()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            attack.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    protected override void Attackplayer()
    {
        _animator.SetBool("Attacking", false);
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            _animator.SetBool("Attacking", true);
            Collider[] enemies = Physics.OverlapSphere(transform.position, 2);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Player"))
                {
                    if (solo)
                    {
                        enemies[i].GetComponent<CharacterThings>().TakeDamage(50, false, false);
                    }
                    else
                    {
                        enemies[i].GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 50, false, false);
                    }
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacksCAC);
        }
    }
}
