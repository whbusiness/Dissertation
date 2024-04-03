using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForQuit : MonoBehaviour
{
    private SaveGameState saveGame;
    public static DateTime timeEnded;

    private void Start()
    {
        saveGame = GetComponent<SaveGameState>();
    }

    private void OnApplicationQuit()
    {
        print("Game Had Quit");
        timeEnded = DateTime.Now;
        saveGame.SaveData();
        print("Saved Data");
    }
}
