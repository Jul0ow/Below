using System.Collections;
using System.Collections.Generic;
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
                    enemies[i].GetComponent<CharacterThings>().TakeDamage(50, false, false);
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacksCAC);
        }
    }
}
