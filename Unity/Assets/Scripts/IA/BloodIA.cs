using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodIA : EnnemyIA
{
    public GameObject Boss;

    protected override void Awake()
    {
        Boss = GameObject.FindGameObjectWithTag("Ennemy");
        agent.speed = speed;
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected override void Update()
    {
        Patroling();
    }
}
