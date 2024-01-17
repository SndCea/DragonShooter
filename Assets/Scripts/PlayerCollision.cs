using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void playerCollided ()
    {
        Debug.Log("Player Collided");
        gameObject.transform.parent.gameObject.GetComponent<PlayerData>().PlayerHit();
    }
}
