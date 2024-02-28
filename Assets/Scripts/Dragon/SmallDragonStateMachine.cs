using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallDragonStateMachine : MonoBehaviour
{
    public MonoBehaviour dragonWalk;
    public MonoBehaviour dragonHit;
    public MonoBehaviour dragonDie;
    public MonoBehaviour dragonBite;
    [SerializeField] bool freezed;
    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!freezed)
        {
            NavMeshHit hit;
            Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            float distance = Vector3.Distance(transform.position, playerPosition.position);

            if (dragonHit.enabled == false && dragonDie.enabled == false)
            {
                if (distance <= GetComponent<DragonData>().scriptableDragon.attackDistance)
                {
                    Bite();
                }
                else
                {
                    Walk();
                }
            }
            

        }
    }

    public void Bite()
    {
        dragonHit.enabled = false;
        dragonDie.enabled = false;

        dragonWalk.enabled = false;

        dragonBite.enabled = true;
    }
    public void Walk()
    {
        dragonHit.enabled = false;
        dragonDie.enabled = false;

        dragonBite.enabled = false;

        dragonWalk.enabled = true;
    }

  

    public void GetHit()
    {
        dragonWalk.enabled = false;
        dragonDie.enabled = false;
        
        dragonBite.enabled = false;
        dragonHit.enabled = true;
    }

    public void Die()
    {
        dragonWalk.enabled = false;
        dragonHit.enabled = false;
        dragonBite.enabled = false;

        dragonDie.enabled = true;
    }

    public void Stop(bool stop)
    {
        Debug.Log("Stop " + stop);

        //Si hay un cambio de script en este momento, se activara
        //ya que pondrá enabled a true

        Animator animator = GetComponent<Animator>();
        agent.isStopped = stop;
        animator.enabled = !stop;
        freezed = stop;

    }
}
