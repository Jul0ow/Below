using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodIA : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Boss;
    public int speed;
    
    protected virtual void Awake()
    {
        agent.speed = speed;
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected void Update()
    {
        agent.SetDestination(Boss.transform.position);
        transform.LookAt(Boss.transform);
    }
}
