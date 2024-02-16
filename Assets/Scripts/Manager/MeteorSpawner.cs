using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject MeteorPrefab;
    private FireBaseScript currentPrefabScript;
    void Start()
    {
        BeginEffect();
    }

    void Update()
    {
        
    }
    private void BeginEffect()
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        Instantiate(MeteorPrefab);
        currentPrefabScript = MeteorPrefab.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = MeteorPrefab.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = MeteorPrefab.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other fire layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
        }

        MeteorPrefab.transform.position = pos;
        MeteorPrefab.transform.rotation = rotation;
    }
}
