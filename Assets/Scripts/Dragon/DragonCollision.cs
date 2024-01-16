using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollision : MonoBehaviour
{
    public bool isAtacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DRAGON COLLIDES WITH " + collision.gameObject.name);
        if (isAtacking && collision.gameObject.transform.parent.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCollision>().playerCollided();
        }
    }
}
