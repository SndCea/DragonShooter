using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonHit : MonoBehaviour
{
    private Animator animator;
    public bool wasFreezed;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        if (animator.enabled == false)
        {
            wasFreezed = true;
            animator.enabled = true;
        }
        animator.SetTrigger("hit");
    }

    void OnDisable()
    {
        wasFreezed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableHit()
    {
        if (wasFreezed)
        {
            GetComponent<DragonData>().Stop(true);
        }
        this.enabled = false;
    }
}
