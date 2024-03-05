using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private void Awake()
    {
        InicializePlayerPrefabs();
    }
    private void InicializePlayerPrefabs()
    {
        int nAtt;
        if (!PlayerPrefs.HasKey("nAttempt"))
        {
            nAtt = 0;
        }
        else
        {
            nAtt = PlayerPrefs.GetInt("nAttempt") + 1;
        }
        PlayerPrefs.SetInt("nAttempt", nAtt);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
