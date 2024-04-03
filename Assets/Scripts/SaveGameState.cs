using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public List<Prefabs> spawnedItemsInScene = new();
    public List<Vector3> spawnedItemsPositions = new();
    public int Score;
    public float sfxVolume, musicVolume, brightness, fontSize, saturation, redValue, greenValue, blueValue;
    public bool isInverted, isSpeechRecognition, isTTS;
    public string language;
    public Color colorFilter;
    public float timeSpentPlaying;
}

public class SaveGameState : MonoBehaviour
{
    public static SaveGameState instance;
    [SerializeField]
    private List<Prefabs> prefabs = new();
    public List<GameObject> gameObjectsInScene = new();
    public SaveData saveData = new SaveData();
    private ScoreTxt scoreTxt;
    private IsSpeechRecogOn saveValues;
    private void Awake()
    {
        saveValues = FindObjectOfType<IsSpeechRecogOn>();
    }
    private void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveData()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level")
        {
            gameObjectsInScene.Clear();
            saveData.spawnedItemsInScene.Clear();
            saveData.spawnedItemsPositions.Clear();
            gameObjectsInScene.AddRange(FindObjectsOfType<GameObject>());
            scoreTxt = FindObjectOfType<ScoreTxt>();
            saveValues = FindObjectOfType<IsSpeechRecogOn>();
            foreach (var gameObject in gameObjectsInScene)
            {
                if (gameObject.CompareTag("Apple"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[0]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Burger"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[1]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Carrot"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[2]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Donut"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[3]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Pumpkin"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[4]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Soda"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[5]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
                if (gameObject.CompareTag("Watermelon"))
                {
                    saveData.spawnedItemsInScene.Add(prefabs[6]);
                    saveData.spawnedItemsPositions.Add(gameObject.transform.position);
                }
            }
            saveData.Score = ScoreTxt.Score;
            print("Time Level Loaded: " + ArrowController.timeLevelWasLoaded);
            print("CurrentTime: " + Time.time);
            var overallTime = Time.time - ArrowController.timeLevelWasLoaded;
            saveData.timeSpentPlaying += overallTime;
        }
        if(currentScene.name == "LostScene")
        {
            print("Clear Items In Scene");
            if(saveData.spawnedItemsInScene.Count > 0)
            {
                saveData.spawnedItemsInScene.Clear();
            }
            if(saveData.spawnedItemsPositions.Count > 0)
            {
                saveData.spawnedItemsPositions.Clear();
            }
            print("Time Level Loaded: " + ArrowController.timeLevelWasLoaded);
            print("CurrentTime: " + Time.time);
            var overallTime = Time.time - ArrowController.timeLevelWasLoaded;
            saveData.timeSpentPlaying += overallTime;
            print("time spent playing after loss: " + saveData.timeSpentPlaying);
            saveData.Score = 0;
            saveData.timeSpentPlaying = 0;
            print("time spent playing loss: " + saveData.timeSpentPlaying);
        }
        saveData.sfxVolume = saveValues.sfxValue;
        saveData.musicVolume = saveValues.musicValue;
        saveData.language = saveValues.whatLanguage;
        saveData.brightness = saveValues.brightnessValue;
        saveData.fontSize = saveValues.fontSizeValue;
        saveData.isInverted = saveValues.isInverted;
        saveData.isSpeechRecognition = saveValues.speechRecogMode;
        saveData.isTTS = saveValues.isTTS;
        saveData.colorFilter = saveValues.colorFilter;
        saveData.saturation = saveValues.saturation;
        saveData.redValue = saveValues.redValue;
        saveData.greenValue = saveValues.greenValue;
        saveData.blueValue = saveValues.blueValue;
        print("Time Spent Playing: " + saveData.timeSpentPlaying);
        SaveDataToJSON();
    }

    public void SaveDataToJSON()
    {
        string savedData = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/SavedData.json";
        Debug.Log(filePath);
        Debug.Log("Save In Progress. Score is " + saveData.Score);
        System.IO.File.WriteAllText(filePath, savedData);
        Debug.Log("Saved Complete. Score is " + saveData.Score);
    }

    public void LoadDataToJSON()
    {
        string filePath = Application.persistentDataPath + "/SavedData.json";
        if (!System.IO.File.Exists(filePath)) return;

        string loadData = System.IO.File.ReadAllText(filePath);

        saveData = JsonUtility.FromJson<SaveData>(loadData);
        Debug.Log("Load Complete. Time Started: " + saveData.timeSpentPlaying);
        saveValues.sfxValue = saveData.sfxVolume;
        saveValues.musicValue = saveData.musicVolume;
        saveValues.whatLanguage = saveData.language;
        saveValues.brightnessValue = saveData.brightness;
        saveValues.fontSizeValue = saveData.fontSize;
        saveValues.isInverted = saveData.isInverted;
        saveValues.speechRecogMode = saveData.isSpeechRecognition;
        saveValues.isTTS = saveData.isTTS;
        saveValues.saturation = saveData.saturation;
        saveValues.colorFilter = saveData.colorFilter;
        saveValues.redValue = saveData.redValue;
        saveValues.greenValue = saveData.greenValue;
        saveValues.blueValue = saveData.blueValue;
    }

    public void LoadSceneObjectsInLevel()
    {
        scoreTxt = FindObjectOfType<ScoreTxt>();
        ScoreTxt.Score = saveData.Score;
        scoreTxt.DisplayScore();
        ScoreTxt.Score = saveData.Score;
        for (int i = 0; i < saveData.spawnedItemsInScene.Count; i++)
        {
            saveData.spawnedItemsInScene[i].SpawnPrefab(saveData.spawnedItemsPositions[i]);
        }
    }
}
