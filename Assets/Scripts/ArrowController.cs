using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class ArrowController : MonoBehaviour
{
    public Vector2 move;
    NewControls controls;
    [SerializeField]
    private float _movement;
    [SerializeField]
    private GameObject[] _prefabs;
    private bool canCreate = true;
    public int nextSpawnObject;
    public static int spawnObject;
    [SerializeField]
    private Transform spawnPoint, _lrEndPoint;
    [SerializeField]
    private float furthestLeftMovement, furthestRightMovement;
    private bool isPaused = false;
    private bool keepMovingLeft = false, keepMovingRight = false;
    [SerializeField]
    private GameObject pauseCanvas, settingsMenu, ControlsDisplay, VolumeDisplay, ColourDisplay;
    [SerializeField]
    private GameObject speechRecogTxt;
    [SerializeField]
    private Toggle speechRecogToggle, ttsToggle;
    [SerializeField]
    private IsSpeechRecogOn speechRecogCheck;
    public KeywordRecognizer _kr;
    private Dictionary<string, Action> spokenWords = new Dictionary<string, Action>();
    private LineRenderer _lr;
    private DisplayNextObject displayObject;
    private ContrastChanger brightnessChanger;
    [SerializeField]
    private Slider brightnessSlider, fontSizeSlider, sfxSlider, musicSlider, redColourSlider, greenColourSlider, blueColourSlider, satSlider;
    private SaveGameState saveGame;
    private FontSizeChanger fontSizeChanger;
    private RebindKeys invertMovement;
    [SerializeField]
    private Prefabs[] foodSpawns;
    [SerializeField]
    private AudioSource[] musicAndSFX;
    string spokenValue;
    public PostProcessProfile profile;
    Color colourFilter;
    ColorGrading colorGrade;
    public static float timeLevelWasLoaded;
    // Start is called before the first frame update
    void Awake()
    {
        saveGame = FindObjectOfType<SaveGameState>();
        _lr = GetComponentInChildren<LineRenderer>();
        displayObject = FindObjectOfType<DisplayNextObject>();
        speechRecogCheck = FindObjectOfType<IsSpeechRecogOn>();
        brightnessChanger = FindObjectOfType<ContrastChanger>();
        fontSizeChanger = FindObjectOfType<FontSizeChanger>();
        invertMovement = FindObjectOfType<RebindKeys>();
        nextSpawnObject = UnityEngine.Random.Range(0, 100);
        PickNextObject();
        displayObject.DisplayObject();
        controls = new NewControls();
        controls.PlayerMap.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.PlayerMap.Movement.canceled += ctx => move = Vector2.zero;
        musicAndSFX = GameObject.FindGameObjectWithTag("CollisionSFX").GetComponents<AudioSource>();
    }
    private void Start()
    {
        timeLevelWasLoaded = Time.time;
        invertMovement.OnInvertMovement();
        _lr.SetPosition(0, spawnPoint.position);
        if (speechRecogCheck.speechRecogMode)
        {
            speechRecogTxt.SetActive(true);
        }
        else
        {
            speechRecogTxt.SetActive(false);
        }
        profile.TryGetSettings(out colorGrade);
        spokenWords.Add("Left", LeftMovement);
        spokenWords.Add("Right", RightMovement);
        spokenWords.Add("Place", OnCreateObject);
        spokenWords.Add("Pause", OnPause);
        spokenWords.Add("Stop", OnStop);
        spokenWords.Add("Settings", OnSettings);
        spokenWords.Add("Menu", OnMainMenu);
        spokenWords.Add("Toggle", OnSpeechRecogToggle);
        spokenWords.Add("Back", OnBack);
        spokenWords.Add("Brightness Up", OnBrightnessUp);
        spokenWords.Add("Brighness Down", OnBrightnessDown);
        spokenWords.Add("Font Up", OnFontUp);
        spokenWords.Add("Font Down", OnFontDown);
        spokenWords.Add("Controls", OnChangeControls);
        spokenWords.Add("Colour", OnColourBtn);
        spokenWords.Add("Volume", OnVolumeBtn);
        spokenWords.Add("SFX Up", OnSFXUp);
        spokenWords.Add("SFX Down", OnSFXDown);
        spokenWords.Add("Music Up", OnMusicUp);
        spokenWords.Add("Music Down", OnMusicDown);
        spokenWords.Add("Red Up", ColourFilterChange);
        spokenWords.Add("Red Down", ColourFilterChange);
        spokenWords.Add("Blue Up", ColourFilterChange);
        spokenWords.Add("Blue Down", ColourFilterChange);
        spokenWords.Add("Green Up", ColourFilterChange);
        spokenWords.Add("Green Down", ColourFilterChange);
        spokenWords.Add("Saturation Up", OnSaturationUp);
        spokenWords.Add("Saturation Down", OnSaturationDown);
        _kr = new KeywordRecognizer(spokenWords.Keys.ToArray());
        _kr.OnPhraseRecognized += SpeechRecognised;
        if (speechRecogCheck.speechRecogMode)
        {
            _kr.Start();
            speechRecogToggle.isOn = true;
        }
        else
        {
            speechRecogToggle.isOn = false;
        }
        brightnessChanger.exposure.keyValue.value = speechRecogCheck.brightnessValue;
        brightnessSlider.value = speechRecogCheck.brightnessValue;
        ttsToggle.isOn = speechRecogCheck.isTTS;
        saveGame.LoadDataToJSON();
        saveGame.LoadSceneObjectsInLevel();
    }

    private void OnEnable()
    {
        controls.PlayerMap.Enable();
    }
    private void OnDisable()
    {
        controls.PlayerMap.Disable();
    }

    void SpeechRecognised(PhraseRecognizedEventArgs e)
    {
        print(e.text);
        spokenValue = e.text;
        if(e.text == "Left")
        {
            keepMovingRight = false;
            keepMovingLeft = true;
        }
        else if(e.text == "Right")
        {
            keepMovingLeft=false;
            keepMovingRight = true;
        }
        spokenWords[e.text].Invoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(move.x != 0)
        {
            MoveArrow();
        }
        if (keepMovingLeft && transform.position.x > furthestLeftMovement && !isPaused)
        {
            keepMovingRight = false;
            transform.position -= invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement / 10, 0);
        }
        else if(keepMovingRight && transform.position.x < furthestRightMovement && !isPaused)
        {
            keepMovingLeft = false;
            transform.position += invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement / 10, 0);
        }
        if(_lr.GetPosition(0) != spawnPoint.position)
        {
            _lr.SetPosition(0, spawnPoint.position);
        }
        if (_lr.GetPosition(1) != _lrEndPoint.position)
        {
            _lr.SetPosition(1, _lrEndPoint.position);
        }

        if (!isPaused)
        {
            if(pauseCanvas.activeInHierarchy || settingsMenu.activeInHierarchy || VolumeDisplay.activeInHierarchy || ControlsDisplay.activeInHierarchy || ColourDisplay.activeInHierarchy)
            {
                isPaused = true;
            }
        }

    }

    void MoveArrow() //-1 left 1 right
    {
        if (invertMovement.movement == 1) //Normal Movement
        {
            if (move.x == -1 && transform.position.x > furthestLeftMovement && !isPaused)
            {
                print("Moving Left");
                transform.position -= invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
            }
            else if (move.x == 1 && transform.position.x < furthestRightMovement && !isPaused)
            {
                print("Moving Right");
                transform.position += invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
            }
        }
        else if (invertMovement.movement == -1) //Inverted Movement
        {
            if(move.x == 1 && transform.position.x > furthestLeftMovement && !isPaused)
            {
                //Moving Left
                print("Inverted Movement Left");
                transform.position += invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
            }
            else if (move.x == -1 && transform.position.x < furthestRightMovement && !isPaused)
            {
                //Moving Right
                print("Inverted Movement Right");
                transform.position -= invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
            }
        }
    }

    void LeftMovement()
    {
        if(transform.position.x > furthestLeftMovement)
        {
            transform.position -= invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
        }
    }
    void RightMovement()
    {
        if (transform.position.x < furthestRightMovement)
        {
            transform.position += invertMovement.movement * Time.fixedDeltaTime * new Vector3(_movement, 0);
        }
    }

    void OnStop()
    {
        keepMovingRight = false;
        keepMovingLeft = false;
    }
    void OnSaturationUp()
    {
        if (satSlider.value <= .9)
        {
            satSlider.value += .1f;
            colorGrade.saturation.value = satSlider.value;
            speechRecogCheck.saturation = satSlider.value;
        }
    }

    void OnSaturationDown()
    {
        if (satSlider.value >= .1)
        {
            satSlider.value -= .1f;
            colorGrade.saturation.value = satSlider.value;
            speechRecogCheck.saturation = satSlider.value;
        }
    }

    public void OnPause()
    {
        if (settingsMenu.activeInHierarchy || ControlsDisplay.activeInHierarchy || VolumeDisplay.activeInHierarchy || ColourDisplay.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
            ControlsDisplay.SetActive(false);
            VolumeDisplay.SetActive(false);
            ColourDisplay.SetActive(false);
        }
        if (isPaused)
        {
            isPaused = false;
            pauseCanvas.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
            pauseCanvas.SetActive(true);
        }
    }

    public void OnVolumeBtn()
    {
        settingsMenu.SetActive(false);
        VolumeDisplay.SetActive(true);
    }

    void OnSFXDown()
    {
        if (sfxSlider.value >= 0.1)
        {
            sfxSlider.value -= .1f;
            musicAndSFX[0].volume = sfxSlider.value;
            speechRecogCheck.sfxValue = sfxSlider.value;
        }
    }

    void OnSFXUp()
    {
        if (sfxSlider.value <= .9)
        {
            sfxSlider.value += .1f;
            musicAndSFX[0].volume = sfxSlider.value;
            speechRecogCheck.sfxValue = sfxSlider.value;
        }
    }

    void OnMusicDown()
    {
        if (musicSlider.value >= 0.1)
        {
            musicSlider.value -= .1f;
            musicAndSFX[1].volume = musicSlider.value;
            speechRecogCheck.musicValue = musicSlider.value;
        }
    }

    void OnMusicUp()
    {
        if (musicSlider.value <= .9)
        {
            musicSlider.value += .1f;
            musicAndSFX[1].volume = musicSlider.value;
            speechRecogCheck.musicValue = musicSlider.value;
        }
    }


    void OnCreateObject()
    {
        if (canCreate && !isPaused)
        {
            print("Next spawn object is : " + spawnObject);
            foodSpawns[spawnObject].SpawnPrefab(spawnPoint.position);
            //GameObject creation = Instantiate(_prefabs[spawnObject], spawnPoint.position, Quaternion.identity);
            StartCoroutine(nameof(DelayNextCreation), 1f);
        }
    }

    IEnumerator DelayNextCreation(float delay)
    {
        canCreate = false;
        yield return new WaitForSeconds(delay);
        canCreate = true;
        nextSpawnObject = UnityEngine.Random.Range(0, 100);
        PickNextObject();
        displayObject.DisplayObject();

    }
    void PickNextObject()
    {
        switch (nextSpawnObject)
        {
            case <= 40://spawn soda
                print("Between 0-30");
                spawnObject = 0;
                break;
            case > 40 and <= 60://spawn carrot
                print("Between 31-60");
                spawnObject = 1;
                break;
            case > 60 and <= 80://spawn apple
                print("Between 61-100");
                spawnObject = 2;
                break;
            case > 80 and <= 90://spawn doughnut
                print("Between 61-100");
                spawnObject = 3;
                break;
            case > 90 and <= 100://spawn burger
                print("Between 61-100");
                spawnObject = 4;
                break;
        }
    }

    public void OnSettings()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
        if(pauseCanvas.activeInHierarchy)
        {
            pauseCanvas.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }
    public void OnMainMenu()
    {
        if(_kr.IsRunning)
        {
            _kr.Stop();
        }
        _kr.OnPhraseRecognized -= SpeechRecognised;
        _kr.Dispose();
        _kr = null;
        saveGame.SaveData();
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSpeechRecogToggle()
    {
        if(settingsMenu.activeInHierarchy)
        {
            if (_kr.IsRunning) { speechRecogToggle.isOn = false; speechRecogTxt.SetActive(false); ; print("Stop Speech Recog"); _kr.Stop(); speechRecogCheck.SpeechRecogOff(); keepMovingLeft = false; keepMovingRight = false; }
            else { speechRecogToggle.isOn = true; speechRecogTxt.SetActive(true); ; print("Start Speech Recog"); _kr.Start(); speechRecogCheck.SpeechRecogOn(); }
        }
    }
    public void OnTTS()
    {
        if (ttsToggle.isOn)
        {
            speechRecogCheck.TTSOn();
        }
        else
        {
            speechRecogCheck.TTSOff();
        }
    }

    public void OnBack()
    {
        if (settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
            pauseCanvas.SetActive(true);
        }
        if (ControlsDisplay.activeInHierarchy)
        {
            ControlsDisplay.SetActive(false);
            settingsMenu.SetActive(true);
        }
        if (VolumeDisplay.activeInHierarchy)
        {
            VolumeDisplay.SetActive(false);
            settingsMenu.SetActive(true);
        }
        if (ColourDisplay.activeInHierarchy)
        {
            ColourDisplay.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }
    public void OnColourBtn()
    {
        settingsMenu.SetActive(false);
        ColourDisplay.SetActive(true);
    }
    void OnBrightnessUp()
    {
        if (brightnessSlider.value < 1)
        {
            brightnessSlider.value += 0.1f;
            speechRecogCheck.brightnessValue = brightnessSlider.value;
        }
    }

    void OnBrightnessDown()
    {
        if (brightnessSlider.value > 0.1f)
        {
            brightnessSlider.value -= 0.1f;
            speechRecogCheck.brightnessValue = brightnessSlider.value;
        }
    }

    void OnFontDown()
    {
        if (fontSizeSlider.value >= 13)
        {
            fontSizeSlider.value -= 1f;
            speechRecogCheck.fontSizeValue = fontSizeSlider.value;
        }
    }

    void ColourFilterChange()
    {
        float value = 0;
        Slider sl = null;
        string Colour = "";

        if (spokenValue == "Red Up")
        {
            value = 1;
            sl = redColourSlider;
            Colour = "Red";
        }
        else if (spokenValue == "Red Down")
        {
            value = -1;
            sl = redColourSlider;
            Colour = "Red";
        }
        else if (spokenValue == "Green Up")
        {
            value = 1;
            sl = greenColourSlider;
            Colour = "Green";
        }
        else if (spokenValue == "Green Down")
        {
            value = -1;
            sl = greenColourSlider;
            Colour = "Green";
        }
        else if (spokenValue == "Blue Up")
        {
            value = 1;
            sl = blueColourSlider;
            Colour = "Blue";
        }
        else if (spokenValue == "Blue Down")
        {
            value = -1;
            sl = blueColourSlider;
            Colour = "Blue";
        }



        print("Slider: " + sl.name + " Value: " + value);

        if (value == 1 && sl.value <= 1.9)
        {
            print("Going UP");
            sl.value += .1f;
        }
        if (value == -1 && sl.value >= .1)
        {
            print("Going Down");
            sl.value -= .1f;
        }

        if (Colour == "Green")
        {
            colourFilter = new Color(colourFilter.r, sl.value, colourFilter.b);
            speechRecogCheck.greenValue = sl.value;
        }
        else if (Colour == "Red")
        {
            colourFilter = new Color(sl.value, colourFilter.g, colourFilter.b);
            speechRecogCheck.redValue = sl.value;
        }
        else if (Colour == "Blue")
        {
            colourFilter = new Color(colourFilter.r, colourFilter.g, sl.value);
            speechRecogCheck.blueValue = sl.value;
        }
        colorGrade.colorFilter.value = colourFilter;
        speechRecogCheck.colorFilter = colourFilter;
    }

    void OnFontUp()
    {
        if (fontSizeSlider.value <= 35)
        {
            fontSizeSlider.value += 1f;
            speechRecogCheck.fontSizeValue = fontSizeSlider.value;
        }
    }

    public void OnSFXSlider(float value)
    {
        value = sfxSlider.value;
        musicAndSFX[0].volume = value;
        speechRecogCheck.sfxValue = value;
    }

    public void OnMusicSlider(float value)
    {
        value = musicSlider.value;
        musicAndSFX[1].volume = value;
        speechRecogCheck.musicValue = value;
    }

    public void OnChangeControls()
    {
        pauseCanvas.SetActive(false);
        settingsMenu.SetActive(false);
        ControlsDisplay.SetActive(true);
    }
}
