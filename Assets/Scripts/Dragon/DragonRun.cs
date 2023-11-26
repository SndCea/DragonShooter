using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonRun : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject goal;
    private Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("run", true);


        agent = GetComponent<NavMeshAgent>();
        agent.speed = 7f;
        goal = GameObject.FindGameObjectWithTag("Player");
        agent.isStopped = false;
    }

    void Update()
    {
        agent.destination = goal.transform.position;
    }

    

    private void OnDisable()
    {
        agent.isStopped = true;
        Debug.Log("Stop run");
        animator.SetBool("run", false);
    }
}
