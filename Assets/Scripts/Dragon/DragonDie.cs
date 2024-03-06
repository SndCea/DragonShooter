using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonDie : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject powerUp;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        powerUp = GetComponent<DragonData>().PowerUp;

        if (animator.enabled == false )
        {
            animator.enabled = true;
        }
        animator.SetTrigger("die");
    }

    void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        if (!GetComponent<DragonData>().stop)
        {
            Instantiate(powerUp, new Vector3(transform.position.x, powerUp.transform.position.y, transform.position.z), Quaternion.identity);
        }
        if (GetComponentInParent<DragonSpawner>() != null)
        {
            GetComponentInParent<DragonSpawner>().CheckDragonsInScene();
        }
    }
}
