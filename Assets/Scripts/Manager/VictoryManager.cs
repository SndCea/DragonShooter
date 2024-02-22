using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager VictoryManagerInstance { get; private set; }
    public GameObject meteorSpawner;
    private void Awake()
    {
        if (VictoryManagerInstance != null && VictoryManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            VictoryManagerInstance = this;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SpawnMeteorShower()
    {
       Instantiate(meteorSpawner);
    }


}
