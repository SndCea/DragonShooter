using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsPage : MonoBehaviour
{
    public List<GameScore> AllGameScores;
    public GameObject ScorePanel;
    public GameObject RowPrefab;
    public GameObject NoRecordsPanel;
    public GameObject ScoreBigPanel;
    private void Awake()
    {
        NoRecordsPanel.SetActive(false);
        AllGameScores = new List<GameScore>();
        GetAllGameScores();
    }
    void Start()
    {
        if (AllGameScores.Count > 0)
        {
            ScoreBigPanel.SetActive(true);
            NoRecordsPanel.SetActive(false);
            OrderGameScores();
            PaintGameScores();
        }
        else
        {
            NoRecordsPanel.SetActive(true);
            ScoreBigPanel.SetActive(false);
        }
    }

    void Update()
    {

    }
    public void GetAllGameScores()
    {

        if (PlayerPrefs.HasKey("nAttempt"))
        {
            int nAttemptsTotal = PlayerPrefs.GetInt("nAttempt") + 1;
            string nAttString;
            for (int i = 0; i < nAttemptsTotal; i++)
            {
                nAttString = "nAttempt_" + i;

                if (PlayerPrefs.HasKey(nAttString))
                {
                    float time = PlayerPrefs.GetFloat(nAttString);
                    string nAttStringWin = "nAttempt_Win_" + i;
                    if (PlayerPrefs.HasKey(nAttStringWin))
                    {
                        int winInt = PlayerPrefs.GetInt(nAttStringWin);

                        bool win = winInt == 1 ? true : false;
                        if (!win) { time = -time; }
                        GameScore gameScore = new GameScore(i, time, win);
                        AllGameScores.Add(gameScore);
                    }
                }
            }
        }
    }
    public void PaintGameScores()
    {
        for (int i = 0; i < AllGameScores.Count; i++)
        {
            GameObject Row = Instantiate(RowPrefab, ScorePanel.transform);

            RecordsRow auxRow = Row.GetComponent<RecordsRow>();
            auxRow.Round = AllGameScores[i].nAttempt.ToString();
            auxRow.Time = AllGameScores[i].time.ToString();
            auxRow.Win = AllGameScores[i].win;
        }

    }

    //Sort by time, but negatives are after positives
    public void OrderGameScores()
    {
        AllGameScores.Sort((x, y) =>
        {
            if (x.time >= 0 && y.time >= 0)
            {
                return x.time.CompareTo(y.time);
            }
            else if (x.time < 0 && y.time < 0)
            {
                return y.time.CompareTo(x.time);
            }
            else if (x.time >= 0 && y.time < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
    }
}

public class GameScore
{
    public int nAttempt;
    public float time;
    public bool win;

    public GameScore(int nAttempt, float time, bool win)
    {
        this.nAttempt = nAttempt;
        this.time = time;
        this.win = win;
    }
}
