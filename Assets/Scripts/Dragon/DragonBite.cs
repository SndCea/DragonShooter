using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBite : MonoBehaviour
{
    private Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("bite", true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCollisionOnAttack()
    {
        GetComponentInChildren<DragonCollision>().isAtacking = true;
    }
    public void SetCollisionOnSleep()
    {
        GetComponentInChildren<DragonCollision>().isAtacking = false;
    }
    private void OnDisable()
    {
        animator.SetBool("bite", false);
    }
}
