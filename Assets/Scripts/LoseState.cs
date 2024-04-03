using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class LoseState : MonoBehaviour
{
    public SaveGameState SaveGameState;
    [SerializeField]
    private TextMeshProUGUI timeTakenTxt, scoreTxt;

    public TimeSpan overallTime;
    private void Start()
    {
        SaveGameState = FindObjectOfType<SaveGameState>();
        var overallTime = Time.time - ArrowController.timeLevelWasLoaded;
        SaveGameState.saveData.timeSpentPlaying += overallTime;
        var timeSpan = TimeSpan.FromSeconds(SaveGameState.saveData.timeSpentPlaying);
        string minSec = string.Format("{0:00}:{1:00}", timeSpan.TotalMinutes, timeSpan.TotalSeconds);
        timeTakenTxt.SetText("Overall Time: " + minSec);
        scoreTxt.SetText("Score: " + ScoreTxt.Score);
        SaveGameState.SaveData();
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
