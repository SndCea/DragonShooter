using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] float lifetime;
    void Start()
    {
        FreezeDragons(true);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        
    }
    private void OnDestroy()
    {
        FreezeDragons(false);
    }

    private void FreezeDragons (bool doIt)
    {
        DragonData [] dragons = GameObject.FindObjectsOfType<DragonData>();
        for (int i = 0; i < dragons.Length; i++)
        {
            dragons[i].Stop(doIt);
        }
    }

}
