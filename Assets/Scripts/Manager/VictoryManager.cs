using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager VictoryManagerInstance { get; private set; }
    public delegate void ExtinctionDelegate();
    public event ExtinctionDelegate Extinction;
    public GameObject meteorSpawner;
    [SerializeField] GameObject canvasVictory;
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

        IEnumerator time = TimeToExtinct();
        StartCoroutine(time);
        
    }
    public void Extinct()
    {
        if (Extinction != null)
        {
            Extinction();
        }
    }

    IEnumerator TimeToExtinct ()
    {
        yield return new WaitForSeconds(3);
        Extinct();
        IEnumerator canvas = TimeToCanvas();
        StartCoroutine(canvas);
    }

    IEnumerator TimeToCanvas()
    {
        yield return new WaitForSeconds(10);

        EnableCanvas();
    }
    public void EnableCanvas()
    {
        canvasVictory.SetActive(true);
        SaveData();
    }

    public void SaveData()
    {
        float gameTime = TimerManager.Instance.GetTime();
        int nAttempt = PlayerPrefs.GetInt("nAttempt");
        string nAttString = "nAttempt_" + nAttempt;
        PlayerPrefs.SetFloat(nAttString, gameTime);
    }

}
