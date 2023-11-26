using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] float totalMinutes;
    [SerializeField] float totalTime;
    [SerializeField] GameObject canvasGOver;

    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverReleased;
    bool gamedOver;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        totalTime = totalMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime <= 0)
        {
            if (GameOverReleased != null && !gamedOver)
            {
                SetCursorState(false);
                GameOverReleased();
                canvasGOver.SetActive(true);
                totalTime = 0.0f;
                gamedOver = true;
            }
        } else
        {
            totalTime -= Time.deltaTime;
        }
        
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
