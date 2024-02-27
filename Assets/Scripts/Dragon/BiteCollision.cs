using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteCollision : MonoBehaviour
{
    private bool onAtack;

    void Awake()
    {
        onAtack = false;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.parent != null && collider.gameObject.transform.tag == "PlayerCapsule" && onAtack)
        {
            int damage = GetComponentInParent<DragonData>().scriptableDragon.damage;
            collider.gameObject.GetComponent<PlayerCollision>().PlayerHit(damage);
        }
    }
    public void EnableCollision()
    {
        onAtack = true;
    }

    public void DisableCollision()
    {
        onAtack = false;
    }
}
