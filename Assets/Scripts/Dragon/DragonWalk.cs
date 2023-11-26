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
        //Debug.Log(agent.isStopped);
        agent.destination = goal.transform.position;

    }
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player");
        agent.speed = 3.5f;
        agent.isStopped = false;

    }

    private void OnDisable()
    {
        agent.isStopped = true;
    }
}
