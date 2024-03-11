using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStartPosition : MonoBehaviour
{
    public GameObject MeteorPositionsParent;
    public GameObject MeteorBall;
    void CreateMeteorBall()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(MeteorBall, randomPosition, Quaternion.identity, this.transform);



    }
    Vector3 GetRandomPosition()
    {
        int index = Random.RandomRange(0, MeteorPositionsParent.transform.childCount - 1);
        return MeteorPositionsParent.transform.GetChild(index).transform.position;
    }
    void Start()
    {
        CreateMeteorBall();
    }

    void Update()
    {
        
    }
}
