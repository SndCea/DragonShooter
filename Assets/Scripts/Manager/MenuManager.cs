using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    List<GameScore> AllGameScores;
    private void Awake()
    {
        AllGameScores = new List<GameScore>();
        GetAllGameScores();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GetAllGameScores()
    {
        if (PlayerPrefs.HasKey("nAttempt"))
        {
            int nAttemptsTotal = PlayerPrefs.GetInt("nAttempt");
            string nAttString;
            for (int i = 0; i < nAttemptsTotal; i++)
            {
                nAttString = "nAttempt_" + i;
                if (PlayerPrefs.HasKey(nAttString))
                {
                    float time = PlayerPrefs.GetFloat(nAttString);
                    int nAtt = i;
                    GameScore gameScore = new GameScore(nAtt, time);
                    AllGameScores.Add(gameScore);
                }
            }
        }
        PrintAllGameScores();


    }

    public void PrintAllGameScores()
    {
        
        for (int i = 0; i < AllGameScores.Count; i++)
        {
            Debug.Log(AllGameScores[i].nAttempt + " " + AllGameScores[i].time);
        }
        

    }
}

public class GameScore 
{
    public int nAttempt;
    public float time;

    public GameScore(int nAttempt, float time)
    {
        this.nAttempt = nAttempt;
        this.time = time;
    }
}
