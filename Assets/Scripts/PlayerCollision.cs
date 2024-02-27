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
    public void PlayerHit(int damage)
    {
        GetComponentInParent<PlayerData>().PlayerHit(damage);
    }
}
