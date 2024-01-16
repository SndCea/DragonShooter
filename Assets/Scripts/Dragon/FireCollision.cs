using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject collided)
    {
        if (collided.gameObject.transform.parent.tag == "Player")
        {
            collided.gameObject.GetComponent<PlayerCollision>().playerCollided();
        }
    }
}
