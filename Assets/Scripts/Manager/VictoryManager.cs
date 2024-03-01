using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager VictoryManagerInstance { get; private set; }
    public delegate void ExtinctionDelegate();
    public event ExtinctionDelegate Extinction;
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
    public void Extinct()
    {
        if (Extinction != null)
        {
            Extinction();
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

        IEnumerator time = TimeToExtinct();
        StartCoroutine(time);
        
    }

    IEnumerator TimeToExtinct ()
    {
        yield return new WaitForSeconds(3);
        Extinct();
    }

}
