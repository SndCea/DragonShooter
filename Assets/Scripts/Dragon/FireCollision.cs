using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject collided)
    {
        if (collided.gameObject.transform.parent.tag == "Player")
        {
            Debug.Log("PLAYER ATTACKED");
        }
    }
}
