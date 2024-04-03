using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown languageSelector;
    [SerializeField]
    private String dropDownContent;
    [SerializeField]
    private IsSpeechRecogOn languageCheck;

    private void Start()
    {
        languageCheck = FindObjectOfType<IsSpeechRecogOn>();
        languageSelector.value = languageSelector.options.FindIndex(option => option.text == languageCheck.whatLanguage);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageSelector.value];
    }
    public void DropDownMenuLanguageChange()
    {
        dropDownContent = languageSelector.options[languageSelector.value].text;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageSelector.value];
        languageCheck.ChangeLanguage(dropDownContent);
    }
}
