using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigDragonStateMachine : MonoBehaviour
{
    public MonoBehaviour dragonWalk;
    public MonoBehaviour dragonRun;
    public MonoBehaviour dragonHit;
    public MonoBehaviour dragonDie;
    public MonoBehaviour dragonFire;
    [SerializeField] bool freezed;

    void Start()
    {
    }

    void Update()
    {
        if (!freezed)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            NavMeshHit hit;
            Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

            float distance = Vector3.Distance(transform.position, playerPosition.position);

            if (dragonHit.enabled == false && dragonDie.enabled == false)
            {
                if (distance < GetComponent<DragonData>().scriptableDragon.attackDistance)
                {
                    Fire();
                }
                else if (distance > GetComponent<DragonData>().scriptableDragon.attackDistance && distance <= GetComponent<DragonData>().scriptableDragon.walkDistance)
                {
                    Walk();
                }
                else if (distance > GetComponent<DragonData>().scriptableDragon.walkDistance && distance <= GetComponent<DragonData>().scriptableDragon.runDistance)
                {
                    Run();
                }
            }
            
        }
    }

    public void Walk()
    {
        dragonHit.enabled = false;
        dragonDie.enabled = false;

        dragonRun.enabled = false;
        dragonFire.enabled = false;

        dragonWalk.enabled = true;
    }

    public void Run()
    {
        dragonWalk.enabled = false;
        dragonHit.enabled = false;
        dragonDie.enabled = false;
        dragonFire.enabled = false;

        dragonRun.enabled = true;
    }

    public void Fire()
    {
        dragonWalk.enabled = false;
        dragonHit.enabled = false;
        dragonDie.enabled = false;
        dragonRun.enabled = false;

        dragonFire.enabled = true;
    }

    public void GetHit()
    {
        dragonWalk.enabled = false;
        dragonDie.enabled = false;
        dragonRun.enabled = false;
        dragonFire.enabled = false;

        dragonHit.enabled = true;
    }

    public void Die()
    {
        dragonWalk.enabled = false;
        dragonHit.enabled = false;
        dragonRun.enabled = false;
        dragonFire.enabled = false;

        dragonDie.enabled = true;
    }

    public void Stop(bool stop)
    {

        //Si hay un cambio de script en este momento, se activara
        //ya que pondrá enabled a true

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        agent.isStopped = stop;
        animator.enabled = !stop;
        freezed = stop;
    }
}
