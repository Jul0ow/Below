using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodIA : EnnemyIA
{
    public GameObject Boss;

    protected override void Awake()
    {
        agent.speed = speed;
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected override void Update()
    {
        agent.SetDestination(Boss.transform.position);
        transform.LookAt(Boss.transform);
    }
}
