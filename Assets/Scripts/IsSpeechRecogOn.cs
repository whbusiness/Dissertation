using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSpeechRecogOn : MonoBehaviour
{
    public static IsSpeechRecogOn instance = null;
    public bool speechRecogMode = true, isTTS = true, isInverted = false;
    public string whatLanguage = "English";
    public float brightnessValue, fontSizeValue, sfxValue, musicValue, saturation, redValue, greenValue, blueValue;
    public Color colorFilter;
    // Start is called before the first frame update
    private void Awake()
    {
        if (brightnessValue == 0)
        {
            brightnessValue = 1;
        }
        if(fontSizeValue == 0)
        {
            fontSizeValue = 24;
        }
        if(sfxValue == 0)
        {
            sfxValue = 1;
        }
        if (musicValue == 0)
        {
            musicValue = 1;
        }
    }
    void Start()
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

    public void SpeechRecogOn()
    {
        speechRecogMode = true;
    }
    public void SpeechRecogOff()
    {
        speechRecogMode = false;
    }
    public void TTSOn()
    {
        isTTS = true;
    }
    public void TTSOff()
    {
        isTTS = false;
    }
    public void InvertOn()
    {
        isInverted = true;
    }
    public void InvertOff()
    {
        isInverted = false;
    }
    public void ChangeLanguage(string lang)
    {
        whatLanguage = lang;
    }
}
