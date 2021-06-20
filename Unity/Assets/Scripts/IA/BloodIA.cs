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
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
        agent.SetDestination(Boss.transform.position);
        transform.LookAt(Boss.transform);
    }
}
