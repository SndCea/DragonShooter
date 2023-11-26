using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int multiplicate;
    [SerializeField] float lifetime;
    void Start()
    {
        if (GameObject.FindObjectOfType<Weapon>() != null)
        {
            GameObject.FindObjectOfType<Weapon>().IncreaseBulletDamage(multiplicate);
        }
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject.FindObjectOfType<Weapon>().OriginalBulletDamage();
    }
}
