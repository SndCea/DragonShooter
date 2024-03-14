using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager VictoryManagerInstance { get; private set; }
    public delegate void ExtinctionDelegate();
    public event ExtinctionDelegate Extinction;
    public GameObject meteorSpawner;
    [SerializeField] GameObject canvasVictory;
    [SerializeField] TextMeshProUGUI canvasRoundText;
    [SerializeField] TextMeshProUGUI canvasTimeText;

    float gameTime;
    int nAttempt;
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

        InicializePlayerPrefabs();
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
        gameTime = TimerManager.Instance.GetTime();
        nAttempt = PlayerPrefs.GetInt("nAttempt");
        canvasRoundText.text = "Round: " + nAttempt;
        canvasTimeText.text = "Time: " + gameTime;
        canvasVictory.SetActive(true);
        SaveData(true);
    }

    public void SaveData(bool win)
    {
        gameTime = TimerManager.Instance.GetTime();
        nAttempt = PlayerPrefs.GetInt("nAttempt");
        string nAttString = "nAttempt_" + nAttempt;
        string nAttStringWin = "nAttempt_Win_" + nAttempt;
        PlayerPrefs.SetFloat(nAttString, gameTime);
        PlayerPrefs.SetInt(nAttStringWin, win ? 1 : 0);
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

}
