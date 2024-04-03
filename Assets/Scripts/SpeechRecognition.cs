using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using Newtonsoft.Json.Linq;
using UnityEngine.Rendering.PostProcessing;
using Unity.VisualScripting;
using static UnityEditor.Localization.LocalizationTableCollection;

public class SpeechRecognition : MonoBehaviour
{
    private KeywordRecognizer _kr;
    private Dictionary<string, Action> spokenWords = new Dictionary<string, Action>();
    private Scene scene;
    public GameObject MainMenuDisplay, SettingMenuDisplay, ControlsDisplay, VolumeDisplay, ColourDisplay;
    public Toggle ToggleSpeech, ttsToggle;
    public GameObject speechRecogTxt;
    [SerializeField]
    private IsSpeechRecogOn speechRecogCheck;
    private ContrastChanger brightnessChanger;
    private FontSizeChanger fontSizeChanger;
    [SerializeField]
    private Slider brightnessSlider, fontSizeSlider, sfxSlider, musicSlider, redColourSlider, greenColourSlider, blueColourSlider, satSlider;
    [SerializeField]
    private AudioSource[] musicAndSFX;
    private SaveGameState saveSettings;
    Color colourFilter;
    ColorGrading colorGrade;
    public PostProcessProfile profile;
    string spokenValue = "";
    private void Awake()
    {
        speechRecogCheck = FindObjectOfType<IsSpeechRecogOn>();
        brightnessChanger = FindObjectOfType<ContrastChanger>();
        fontSizeChanger = FindObjectOfType<FontSizeChanger>();
        brightnessSlider = brightnessChanger.contrastSlider;
        fontSizeSlider = fontSizeChanger.fontSizeSlider;
        profile.TryGetSettings(out colorGrade);
        saveSettings = FindObjectOfType<SaveGameState>();
        musicAndSFX = GameObject.FindGameObjectWithTag("CollisionSFX").GetComponents<AudioSource>();
        spokenWords.Add("Start", OnStart);
        spokenWords.Add("Settings", OnSettings);
        spokenWords.Add("Quit", OnQuit);
        spokenWords.Add("Back", OnBack);
        spokenWords.Add("Speech", OnSpeechRecognitionToggle);
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
        _kr.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        brightnessChanger.exposure.keyValue.value = speechRecogCheck.brightnessValue;
        brightnessSlider.value = speechRecogCheck.brightnessValue;
        ToggleSpeech.isOn = speechRecogCheck.speechRecogMode;
        ttsToggle.isOn = speechRecogCheck.isTTS;
        if (speechRecogCheck.speechRecogMode)
        {
            speechRecogTxt.SetActive(true);
        }
        else
        {
            speechRecogTxt.SetActive(false);
        }
        scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            MainMenuDisplay.SetActive(true);
            SettingMenuDisplay.SetActive(false);
        }
        saveSettings.LoadDataToJSON();
    }

    void SpeechRecognised(PhraseRecognizedEventArgs e)
    {
        print(e.text);
        spokenValue = e.text;
        spokenWords[e.text].Invoke();
    }

    public void OnStart()
    {
        _kr.Stop();
        _kr.OnPhraseRecognized -= SpeechRecognised;
        _kr.Dispose();
        _kr = null;
        SceneManager.LoadScene("Level");
        saveSettings.SaveData();
    }
    public void OnSettings()
    {
        MainMenuDisplay.SetActive(false);
        SettingMenuDisplay.SetActive(true);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnBack()
    {
        if (SettingMenuDisplay.activeInHierarchy)
        {
            SettingMenuDisplay.SetActive(false);
            MainMenuDisplay.SetActive(true);
        }
        if (ControlsDisplay.activeInHierarchy)
        {
            ControlsDisplay.SetActive(false);
            SettingMenuDisplay.SetActive(true);
        }
        if (VolumeDisplay.activeInHierarchy)
        {
            VolumeDisplay.SetActive(false);
            SettingMenuDisplay.SetActive(true);
        }
        if (ColourDisplay.activeInHierarchy)
        {
            ColourDisplay.SetActive(false);
            SettingMenuDisplay.SetActive(true);
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

    public void OnSpeechRecognitionToggle()
    {
        if (SettingMenuDisplay.activeInHierarchy)
        {
            if (_kr.IsRunning) { ToggleSpeech.isOn = false; speechRecogTxt.SetActive(false); ; print("Stop Speech Recog"); _kr.Stop(); speechRecogCheck.SpeechRecogOff(); }
            else {ToggleSpeech.isOn = true; speechRecogTxt.SetActive(true); print("Start Speech Recog"); _kr.Start(); speechRecogCheck.SpeechRecogOn(); }
        }
    }

    void OnBrightnessUp()
    {
        if(brightnessSlider.value < 1)
        {
            brightnessSlider.value += 0.1f;
            speechRecogCheck.brightnessValue = brightnessSlider.value;
        }
    }

    void OnBrightnessDown()
    {
        if(brightnessSlider.value > 0.1f)
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

    void OnFontUp()
    {
        if (fontSizeSlider.value <= 35)
        {
            fontSizeSlider.value += 1f;
            speechRecogCheck.fontSizeValue = fontSizeSlider.value;
        }
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
        else if(spokenValue == "Red Down")
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

        if (value == 1 && sl.value <=1.9)
        {
            print("Going UP");
            sl.value += .1f;
        }
        if(value == -1 && sl.value >= .1)
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


    public void OnChangeControls()
    {
        MainMenuDisplay.SetActive(false);
        SettingMenuDisplay.SetActive(false);
        ControlsDisplay.SetActive(true);
    }

    public void OnVolumeBtn()
    {
        MainMenuDisplay.SetActive(false);
        SettingMenuDisplay.SetActive(false);
        VolumeDisplay.SetActive(true);
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

    public void OnColourBtn()
    {
        SettingMenuDisplay.SetActive(false);
        ColourDisplay.SetActive(true);
    }

}
