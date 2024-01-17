using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] GameObject canvasGOver;

    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverReleased;
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
    }

    void Update()
    {
    }
    public void GameOver()
    {
        if (GameOverReleased != null)
        {
            SetCursorState(false);
            GameOverReleased();
            canvasGOver.SetActive(true);
        }
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
