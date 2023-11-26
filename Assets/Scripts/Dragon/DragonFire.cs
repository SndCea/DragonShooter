using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonFire : MonoBehaviour
{
    private Animator animator;
    public GameObject fireParticles;
    public GameObject dragonMouth;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("fire", true);
    }

    void Update()
    {
    }

    public void Fire()
    {
        Quaternion rotation = Quaternion.LookRotation(dragonMouth.transform.forward);
        Instantiate(fireParticles, dragonMouth.transform.position, rotation, dragonMouth.transform);
    }

    public void DestroyFire()
    {
        if (dragonMouth.transform.childCount > 0)
        {
            Destroy(dragonMouth.transform.GetChild(0).gameObject);
        }
        
    }



    private void OnDisable()
    {
        DestroyFire();
        animator.SetBool("fire", false);
    }
}
