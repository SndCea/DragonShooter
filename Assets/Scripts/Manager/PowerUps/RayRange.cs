using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayRange : MonoBehaviour
{
    [SerializeField] int rayDistance;
    [SerializeField] float lifetime;
    void Start()
    {
        IncreaseRayRange(true);
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        IncreaseRayRange(false);
    }

    private void IncreaseRayRange(bool doIt)
    {
        Weapon weapon = GameObject.FindObjectOfType<Weapon>();
        ReticlePointer reticle = GameObject.FindObjectOfType<ReticlePointer>();
        
        
        if (doIt)
        {
            weapon.IncreaseRayRange(rayDistance);
        } else
        {
            weapon.OriginalRayRange();
        }
        reticle.UpdateRay();
    }
}
