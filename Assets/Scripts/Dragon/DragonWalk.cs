using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonWalk : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject goal;

    void Update()
    {
        agent.destination = goal.transform.position;

    }
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player");
        agent.isStopped = false;

    }

    private void OnDisable()
    {
        agent.isStopped = true;
    }
}
