using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonSpawner : MonoBehaviour
{
    public UnityEvent<int> OnTriggerEvent;
    private static int nextColliderID = 0;

    public GameObject BigDragon;
    public GameObject MediumDragon;
    public GameObject SmallDragon;

    public Vector2 terrainSize = new Vector2(500f, 500f);
    public int numQuadrants;
    public float fixedHeight = 20f;
    public GameObject DragonPositionsParent;
    private List <Vector3> dragonPositions;

    private void Awake()
    {
        CreateQuadrants();
        GetPositions();
    }
    void Start()
    {

        
    }
    void GetPositions()
    {
        for (int i = 0; i < DragonPositionsParent.transform.childCount; i++)
        {
            dragonPositions.Add(DragonPositionsParent.transform.GetChild(i).position);
        }
    }
    void OnTriggerHandler(int colliderID)
    {
        Debug.Log("Trigger Collision Detected with BoxCollider ID: " + colliderID);
    }

    void CreateQuadrants()
    {
        Vector3 quadrantSize = new Vector3(terrainSize.x / numQuadrants, fixedHeight, terrainSize.y / numQuadrants);

        for (int row = 0; row < numQuadrants; row++)
        {
            for (int col = 0; col < numQuadrants; col++)
            {
                Vector3 childPosition = new Vector3(col * quadrantSize.x, 0f, row * quadrantSize.z);

                GameObject childObject = new GameObject("TerrainQuadrant_" + row + "_" + col);
                childObject.transform.parent = transform;

                BoxCollider boxCollider = childObject.AddComponent<BoxCollider>();
                boxCollider.size = quadrantSize;
                boxCollider.center = childPosition + quadrantSize / 2f;
                boxCollider.isTrigger = true;

                TriggerListener listener = boxCollider.gameObject.AddComponent<TriggerListener>();
                nextColliderID++;
                listener.colliderID = nextColliderID;
                listener.OnTriggerEnterEvent.AddListener(id => OnTriggerHandler(id));

            }
        }
    }
    void Update()
    {
        
    }
    
}





public class TriggerListener : MonoBehaviour
{
    public UnityEvent<int> OnTriggerEnterEvent = new UnityEvent<int>();
    public int colliderID;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("PlayerCapsule"))
        {
            OnTriggerEnterEvent.Invoke(colliderID);
        }
        
    }
}
