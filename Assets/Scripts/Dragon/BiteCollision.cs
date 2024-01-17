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
        if (collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.tag == "Player" && onAtack)
        {
            collider.gameObject.GetComponent<PlayerCollision>().playerCollided();
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
