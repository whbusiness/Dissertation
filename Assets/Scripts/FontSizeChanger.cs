using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FontSizeChanger : MonoBehaviour
{
    public Slider fontSizeSlider, brightnessSlider, sfxSlider, musicSlider;
    public Toggle invertToggle, speechRecogToggle, ttsToggle;
    [SerializeField]
    private List<TextMeshProUGUI> textMeshProUGUIs = new();
    [SerializeField]
    private GameObject settingsPanel, pausePannel, controlsPanel, volumePanel, colorPanel;
    [SerializeField]
    private int amount;
    private IsSpeechRecogOn saveFontValue;
    private void Start()
    {
        saveFontValue = FindObjectOfType<IsSpeechRecogOn>();
        textMeshProUGUIs.AddRange(FindObjectsOfType<TextMeshProUGUI>());
        fontSizeSlider.value = saveFontValue.fontSizeValue;
        OnFontSizeChange(saveFontValue.fontSizeValue);
        brightnessSlider.value = saveFontValue.brightnessValue;
        sfxSlider.value = saveFontValue.sfxValue;
        musicSlider.value = saveFontValue.musicValue;
        ttsToggle.isOn = saveFontValue.isTTS;
        speechRecogToggle.isOn = saveFontValue.speechRecogMode;
        invertToggle.isOn = saveFontValue.isInverted;
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        volumePanel.SetActive(false);
        colorPanel.SetActive(false);
        var scene = SceneManager.GetActiveScene();
        if(scene.name == "Level")
        {
            pausePannel.SetActive(false);
        }
    }

    public void OnFontSizeChange(float value)
    {
        value = fontSizeSlider.value;
        saveFontValue.fontSizeValue = fontSizeSlider.value;
        foreach(var textsize in textMeshProUGUIs)
        {
            textsize.fontSize = value;
            textsize.GetComponent<RectTransform>().sizeDelta = new Vector2(textsize.fontSize * amount, textsize.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

}
