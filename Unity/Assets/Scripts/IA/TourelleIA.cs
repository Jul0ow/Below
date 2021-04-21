using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleIA : EnnemyIA
{
    protected virtual void Patroling()
    {
        transform.LookAt(player.transform);
    }
    
    protected virtual void Chaseplayer()
    {
        transform.LookAt(player.transform);
    }
    
    protected virtual void Attackplayer()
    {
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            attack.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
}
