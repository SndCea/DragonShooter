using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private float actualTime;
    public TextMeshProUGUI timerText;
    private bool stop;
    void Start()
    {
        InicializeDelegates();
    }
    private void OnEnable()
    {
        actualTime = 0;
        stop = false;
    }
    private void InicializeDelegates()
    {
        Debug.Log("HAGO");
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.GameOverReleased += Stop;
        }

        if (VictoryManager.VictoryManagerInstance != null)
        {
            VictoryManager.VictoryManagerInstance.Extinction += Stop;
        }
    }
    private void OnDisable()
    {
        GameOverManager.Instance.GameOverReleased -= Stop;
        VictoryManager.VictoryManagerInstance.Extinction -= Stop;
    }

    void Update()
    {
        if (!stop)
        {
            actualTime += Time.deltaTime;

            UpdateTimerText();
        }
       
    }

    public void Stop()
    {
        stop = true;
    }
    void UpdateTimerText()
    {
        string minutes = Mathf.Floor(actualTime / 60).ToString("00");
        string seconds = (actualTime % 60).ToString("00");

        timerText.text = $"{minutes}:{seconds}";
    }
}
