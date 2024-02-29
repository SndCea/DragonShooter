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

    void Update()
    {

        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
    public void EnableBite()
    {
        GetComponentInChildren<BiteCollision>().EnableCollision();
    }
    public void DisableBite()
    {
        GetComponentInChildren<BiteCollision>().DisableCollision();
    }
    private void OnDisable()
    {
        animator.SetBool("bite", false);
    }
}
