using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTxt : MonoBehaviour
{
    public static int Score;
    private TextMeshProUGUI txtScore;
    private SaveData savaData = new();
    private void Awake()
    {
        txtScore = GetComponent<TextMeshProUGUI>();
    }

    public void OnScoreUpdate(int ValueIncrease)
    {
        Score += ValueIncrease;
        txtScore.text = "Score: " + Score;
    }

    public void DisplayScore()
    {
        txtScore.text = "Score: " + Score;
    }
}
