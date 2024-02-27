using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    private bool hurt;

    void Start()
    {
        hurt = true;
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject collided)
    {
        if (collided.gameObject.transform.tag == "PlayerCapsule" && hurt)
        {
            int damage = GetComponentInParent<Firebase>().damage;
            collided.gameObject.GetComponent<PlayerCollision>().PlayerHit(damage);
            Debug.Log("Mi damage " + GetComponentInParent<DragonData>().scriptableDragon.damage);
            hurt = false;
        }
    }
}
