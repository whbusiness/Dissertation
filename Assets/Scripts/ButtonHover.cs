using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour
{
    [SerializeField]
    private IsSpeechRecogOn languageDetector;
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip englishTTS, frenchTTS, germanTTS, spanishTTS, chineseTTS, japaneseTTS;
    [SerializeField]
    private AudioClip ttsEnglish, ttsFrench, ttsGerman, ttsSpanish, ttsChinese, ttsJapanese;
    [SerializeField]
    private AudioClip germanSettingsMenu, frenchSettingsMenu, spanishSettingsMenu, chineseSettingsMenu, japaneseSettingsMenu, englishSettingsMenu;
    [SerializeField]
    private AudioClip englishFontTTS, spanishFontTTS, germanFontTTS, frenchFontTTS, chineseFontTTS, japaneseFontTTS;

    private void Start()
    {
        languageDetector = FindObjectOfType<IsSpeechRecogOn>();
    }

    public void HoverOverEnter(GameObject go)
    {
        if (!languageDetector.isTTS) return;
        StopCoroutine(nameof(Delay));
        string goName = go.name;
        print(goName);
        switch (languageDetector.whatLanguage)
        {
            case "English":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsEnglish;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if (goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = englishTTS;
                    }
                    if(goName == "Resume")
                    {
                        m_AudioSource.clip = englishSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = englishSettingsMenu;
                        m_AudioSource.time = 1.7f;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                }
                break;
            case "French":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsFrench;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if (goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = frenchTTS;
                    }
                    if (goName == "Resume")
                    {
                        m_AudioSource.clip = frenchSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = frenchSettingsMenu;
                        m_AudioSource.time = 1.7f;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                }
                break;
            case "Spanish":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsSpanish;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if (goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = spanishTTS;
                    }
                    if (goName == "Resume")
                    {
                        m_AudioSource.clip = spanishSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = spanishSettingsMenu;
                        m_AudioSource.time = 1.7f;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                }
                break;
            case "Chinese":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsChinese;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if (goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = chineseTTS;
                    }
                    if (goName == "Resume")
                    {
                        m_AudioSource.clip = chineseSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = chineseSettingsMenu;
                        m_AudioSource.time = 1.1f;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                }

                if (goName == "Start")
                {
                    m_AudioSource.time = 0;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "Settings")
                {
                    m_AudioSource.time = 1.2f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "Quit")
                {
                    m_AudioSource.time = 2.4f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "SpeechRecognitionToggle")
                {
                    m_AudioSource.time = 3.4f;
                    StartCoroutine(nameof(Delay), 1.6f);
                }
                if (goName == "LanguageSelector")
                {
                    m_AudioSource.time = 5f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "BackBtn")
                {
                    m_AudioSource.time = 6f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "BrightnessSlider")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 0f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if (goName == "FontSizeSlider")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 1f;
                    StartCoroutine(nameof(Delay), 1.2f);
                }
                if (goName == "RemapControls")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 2.4f;
                    StartCoroutine(nameof(Delay), 1.1f);
                }
                if (goName == "ColourChange")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 4f;
                    StartCoroutine(nameof(Delay), 1.2f);
                }
                if (goName == "VolumeBtn")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 5.5f;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                if (goName == "Invert")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 7f;
                    StartCoroutine(nameof(Delay), 1.1f);
                }
                if (goName == "PlaceObject")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 8.5f;
                    StartCoroutine(nameof(Delay), 1.2f);
                }
                if (goName == "MusicVolume")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 11.5f;
                    StartCoroutine(nameof(Delay), 1.3f);
                }
                if (goName == "SFXVolume")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 10f;
                    StartCoroutine(nameof(Delay), 1.3f);
                }
                if (goName == "RedColorFilter")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 13f;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                if (goName == "BlueColorFilter")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 14.5f;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                if (goName == "GreenColorFilter")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 16.5f;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                if (goName == "Saturation")
                {
                    m_AudioSource.clip = chineseFontTTS;
                    m_AudioSource.time = 18f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                break;
            case "Japanese":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsJapanese;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if (goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = japaneseTTS;
                    }
                    if (goName == "Resume")
                    {
                        m_AudioSource.clip = japaneseSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = japaneseSettingsMenu;
                        m_AudioSource.time = 1.7f;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                }
                break;
            case "German":
                if (goName == "TTS Toggle")
                {
                    m_AudioSource.clip = ttsGerman;
                    StartCoroutine(nameof(Delay), 1.5f);
                }
                else
                {
                    if(goName != "Resume" || goName != "Main Menu")
                    {
                        m_AudioSource.clip = germanTTS;
                    }
                    if (goName == "Resume")
                    {
                        m_AudioSource.clip = germanSettingsMenu;
                        m_AudioSource.time = 0;
                        StartCoroutine(nameof(Delay), 1f);
                    }
                    if (goName == "Main Menu")
                    {
                        m_AudioSource.clip = germanSettingsMenu;
                        m_AudioSource.time = 1.7f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                }
                break;

        }
        if(languageDetector.whatLanguage != "Chinese")
        {
            if (goName == "Start")
            {
                m_AudioSource.time = 0;
                StartCoroutine(nameof(Delay), 1f);
            }
            else if (goName == "Settings")
            {
                print("SETTINGS HOVERED");
                m_AudioSource.time = 1.4f;
                StartCoroutine(nameof(Delay), 1f);
            }
            else if (goName == "Quit")
            {
                m_AudioSource.time = 3;
                StartCoroutine(nameof(Delay), 1f);
            }
            else if (goName == "SpeechRecognitionToggle")
            {
                m_AudioSource.time = 4.5f;
                StartCoroutine(nameof(Delay), 2.1f);
            }
            else if (goName == "LanguageSelector")
            {
                m_AudioSource.time = 6.7f;
                StartCoroutine(nameof(Delay), 1f);
            }
            else if (goName == "BackBtn")
            {
                m_AudioSource.time = 8f;
                StartCoroutine(nameof(Delay), 1.8f);
            }
            else
            {
                if(languageDetector.whatLanguage == "English")
                {
                    m_AudioSource.clip = englishFontTTS;
                }else if(languageDetector.whatLanguage == "Spanish")
                {
                    m_AudioSource.clip = spanishFontTTS;
                }
                else if(languageDetector.whatLanguage == "German")
                {
                    m_AudioSource.clip = germanFontTTS;
                }
                else if(languageDetector.whatLanguage == "French")
                {
                    m_AudioSource.clip = frenchFontTTS;
                }
                else if(languageDetector.whatLanguage == "Japanese")
                {
                    m_AudioSource.clip = japaneseFontTTS;
                }

                if(goName == "BrightnessSlider")
                {
                    m_AudioSource.time = 0f;
                    StartCoroutine(nameof(Delay), 1f);
                }
                if(goName == "FontSizeSlider")
                {
                    m_AudioSource.time = 1f;
                    StartCoroutine(nameof(Delay), 1.7f);
                }
                if(goName == "RemapControls")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 3.5f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                    else
                    {
                        m_AudioSource.time = 2.9f;
                        StartCoroutine(nameof(Delay), 2.2f);
                    }
                }
                if(goName == "ColourChange")
                {
                    if(languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 5.6f;
                        StartCoroutine(nameof(Delay), 1.6f);

                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 5f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else
                    {
                        m_AudioSource.time = 5.1f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                }
                if(goName == "VolumeBtn")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 7.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 7f;
                        StartCoroutine(nameof(Delay), 2f);
                    }
                    else
                    {
                        m_AudioSource.time = 6.7f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                }
                if (goName == "Invert")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 9.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 9.5f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                    else
                    {
                        m_AudioSource.time = 8.5f;
                        StartCoroutine(nameof(Delay), 1.9f);
                    }
                }
                if (goName == "PlaceObject")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 11.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 11.3f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                    else
                    {
                        m_AudioSource.time = 10.5f;
                        StartCoroutine(nameof(Delay), 2.1f);
                    }
                }
                if (goName == "MusicVolume")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 15.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if (languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 16f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 15.4f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 15.2f;
                        StartCoroutine(nameof(Delay), 1.3f);
                    }
                    else
                    {
                        m_AudioSource.time = 14.5f;
                        StartCoroutine(nameof(Delay), 2f);
                    }
                }
                if (goName == "SFXVolume")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 13.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if(languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 13f;
                        StartCoroutine(nameof(Delay), 2.3f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 13.2f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 13.9f;
                        StartCoroutine(nameof(Delay), 1.3f);
                    }
                    else
                    {
                        m_AudioSource.time = 12f;
                        StartCoroutine(nameof(Delay), 2f);
                    }
                }
                if (goName == "RedColorFilter")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 17.7f;
                        StartCoroutine(nameof(Delay), 1.9f);

                    }
                    else if (languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 18f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 17.5f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 17.4f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                    else
                    {
                        m_AudioSource.time = 16.5f;
                        StartCoroutine(nameof(Delay), 1.6f);
                    }
                }
                if (goName == "BlueColorFilter")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 19.9f;
                        StartCoroutine(nameof(Delay), 1.9f);

                    }
                    else if (languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 20.2f;
                        StartCoroutine(nameof(Delay), 1.8f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 19.5f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 20f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else
                    {
                        m_AudioSource.time = 18.5f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                }
                if (goName == "GreenColorFilter")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 21.9f;
                        StartCoroutine(nameof(Delay), 1.9f);

                    }
                    else if (languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 22.4f;
                        StartCoroutine(nameof(Delay), 2f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 21.5f;
                        StartCoroutine(nameof(Delay), 1.5f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 22.1f;
                        StartCoroutine(nameof(Delay), 1.9f);
                    }
                    else
                    {
                        m_AudioSource.time = 20f;
                        StartCoroutine(nameof(Delay), 1.8f);
                    }
                }
                if (goName == "Saturation")
                {
                    if (languageDetector.whatLanguage == "French")
                    {
                        m_AudioSource.time = 24.5f;
                        StartCoroutine(nameof(Delay), 1.7f);

                    }
                    else if (languageDetector.whatLanguage == "Spanish")
                    {
                        m_AudioSource.time = 24.6f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                    else if (languageDetector.whatLanguage == "German")
                    {
                        m_AudioSource.time = 23.5f;
                        StartCoroutine(nameof(Delay), 1.2f);
                    }
                    else if (languageDetector.whatLanguage == "Japanese")
                    {
                        m_AudioSource.time = 24.9f;
                        StartCoroutine(nameof(Delay), 1.2f);
                    }
                    else
                    {
                        m_AudioSource.time = 22.5f;
                        StartCoroutine(nameof(Delay), 1.7f);
                    }
                }
            }
        }

    }
    public void HoverOverLeave()
    {
        print("Not Hovering");
        StopCoroutine(nameof(Delay));
        m_AudioSource.time = 0;
        m_AudioSource.Stop();
    }

    IEnumerator Delay(float sec)
    {
        print("On Delay");
        m_AudioSource.Play();
        yield return new WaitForSecondsRealtime(sec);
        print("Delay Over");
        m_AudioSource.time = 0;
        m_AudioSource.Stop();
    }
}
