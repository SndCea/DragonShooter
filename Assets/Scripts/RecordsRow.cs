using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordsRow : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timeText;

    private string round;
    private string time;
    private bool win;

    public string Round { get => round; set => round = value; }
    public string Time { get => time; set => time = value; }
    public bool Win { get => win; set => win = value; }

    void Start()
    {
        roundText.text = round;
        Debug.Log("Soy round " + round + " y llega win " + win);
        if (!win)
        {
            timeText.color = Color.red;
            time = "-" + time;
        } else
        {
            timeText.color = Color.white;
        }
        timeText.text = time;
    }
    void Update()
    {
        
    }
}
