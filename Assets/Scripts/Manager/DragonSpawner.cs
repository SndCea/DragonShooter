using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using Random = UnityEngine.Random;

public class DragonSpawner : MonoBehaviour
{
    public UnityEvent<int> OnTriggerEvent;
    private static int nextColliderID = 0;

    public GameObject BigDragon;
    public GameObject MediumDragon;
    public GameObject SmallDragon;
    public int numTotalDragons;
    public float numPercentBigDragon;
    public float numPercentMediumDragon;
    private List<int> usedPosIndexes;

    public Vector2 terrainSize = new Vector2(500f, 500f);
    public int numQuadrants;
    public float fixedHeight = 20f;
    public GameObject DragonPositionsParent;
    private Vector3 [] dragonPositions;

    private void Awake()
    {
        
        GetPositions();
    }

    
    void Start()
    {
        CreateDragons();
        CreateQuadrants();

    }
    void CreateDragons()
    {
        numTotalDragons = Mathf.Min(numTotalDragons, dragonPositions.Length);
        usedPosIndexes = new List<int>();
        float numBigDragons = Mathf.Round(numPercentBigDragon * numTotalDragons);
        float numMediumDragons = Mathf.Round(numPercentMediumDragon * numTotalDragons);
        float numSmallDragons = numTotalDragons - numMediumDragons - numBigDragons;

        for (int i = 0; i < numTotalDragons; i++)
        {
            GameObject Dragon = SmallDragon;
            if (i < numSmallDragons)
            {
                Dragon = SmallDragon;
            }
            else if (i < (numSmallDragons + numMediumDragons))
            {
                Dragon = MediumDragon;
            }
            else if (i < numTotalDragons)
            {
                Dragon = BigDragon;
            }
            int minSpeed = Dragon.GetComponent<DragonData>().scriptableDragon.minSpeed;
            int maxSpeed = Dragon.GetComponent<DragonData>().scriptableDragon.maxSpeed;
            int speed = Random.RandomRange(minSpeed, maxSpeed);
            Debug.Log("Speed: " + speed + " entre: " + minSpeed + " y " + maxSpeed);
            Dragon.GetComponent<NavMeshAgent>().speed = speed;

            Instantiate(Dragon, dragonPositions[GetNextPosIndex()], Quaternion.identity, this.transform);

        }
        usedPosIndexes.Clear();

    }

    private int GetNextPosIndex()
    {
        for (int i = 0; i < dragonPositions.Length; i++)
        {
            int indexPosition = Random.RandomRange(0, dragonPositions.Length);
            if (!usedPosIndexes.Contains(indexPosition))
            {
                usedPosIndexes.Add(indexPosition);
                return indexPosition;
            }
        }
        return 0;

    }

    void GetPositions()
    {
        dragonPositions = new Vector3[DragonPositionsParent.transform.childCount];
        for (int i = 0; i < DragonPositionsParent.transform.childCount; i++)
        {
            Transform child = DragonPositionsParent.transform.GetChild(i).transform;
            dragonPositions[i] = child.transform.position;
        }
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

    void OnTriggerHandler(int colliderID)
    {

    }

}





public class TriggerListener : MonoBehaviour
{
    public UnityEvent<int> OnTriggerEnterEvent = new UnityEvent<int>();
    public int colliderID;
    public bool used;

    void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            BoxCollider detectionCollider = GetComponent<BoxCollider>();

            if (other.gameObject.transform.tag.Equals("PlayerCapsule"))
            {
                used = true;

                Collider[] objInside = Physics.OverlapBox(detectionCollider.center, detectionCollider.size / 2);

                foreach (Collider collider in objInside)
                {
                    if (collider.gameObject.transform.tag.Equals("Dragon") )
                    {
                        //collider.gameObject.transform.parent.GetComponent<DragonData>().Stop(false);
                        collider.gameObject.GetComponentInParent<DragonData>().Stop(false);

                    } else if (collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.transform.tag.Equals("Dragon"))
                    {
                        collider.gameObject.GetComponentInParent<DragonData>().Stop(false);

                    }
                }
                OnTriggerEnterEvent.Invoke(colliderID);
            }
        }
        
        
    }
}
