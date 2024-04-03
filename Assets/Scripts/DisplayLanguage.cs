using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class LanguageOptions
{
    public const String English = "English";
    public const String EnglishGreatestEnemy = "I am your greatest Enemy! Fear Me! Fight Me!";
    public const String EnglishEnough = "Enough Of This! This ends now!";
    public const String Spanish = "Spanish";
    public const String SpanishGreatestEnemy = "¡Soy tu mayor enemigo! ¡Tememe! ¡Lucha conmigo!";
    public const String SpanishEnough = "¡Basta de esto! ¡Esto termina ahora!";
    public const String French = "French";
    public const String FrenchGreatestEnemy = "Je suis ton plus grand ennemi ! Crains moi! Combat moi!";
    public const String FrenchEnough = "Assez de ça! Cela se termine maintenant!";
    public const String Mandarin = "Mandarin";
    public const String MandarinGreatestEnemy = "我是你最大的敌人！怕我！跟我战斗吧！";
    public const String MandarinEnough = "够了！这件事现在结束了！";
    public const String Japanese = "Japanese";
    public const String JapaneseGreatestEnemy = "私はあなたの最大の敵です！恐れろよ！私と戦ってください！";
    public const String JapaneseEnough = "これで十分です！これで終わりです！";
    public const String German = "German";
    public const String GermanGreatestEnemy = "Ich bin dein größter Feind! Fürchte mich! Bekämpfe mich!";
    public const String GermanEnough = "Genug davon! Das ist jetzt vorbei!";
}

public class DisplayLanguage : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown languageSelector;
    [SerializeField]
    private String dropDownContent;
    [SerializeField]
    private AudioSource aiCinematicSource;
    [SerializeField]
    private AudioClip englishGreatest, spanishGreatest, frenchGreatest, mandarinGreatest, japaneseGreatest, germanGreatest;
    [SerializeField]
    private TextMeshProUGUI textDisplayed;
    // Start is called before the first frame update
    void Start()
    {
        aiCinematicSource = GetComponent<AudioSource>();
        dropDownContent = languageSelector.options[languageSelector.value].text;
        if(dropDownContent == LanguageOptions.English)
        {
            textDisplayed.text = LanguageOptions.EnglishGreatestEnemy;
            aiCinematicSource.clip = englishGreatest;
            aiCinematicSource.Play();
        }
        else if(dropDownContent == LanguageOptions.Spanish)
        {
            textDisplayed.text = LanguageOptions.SpanishGreatestEnemy;
            aiCinematicSource.clip = spanishGreatest;
            aiCinematicSource.Play();
        }
        else if (dropDownContent == LanguageOptions.French)
        {
            textDisplayed.text = LanguageOptions.FrenchGreatestEnemy;
            aiCinematicSource.clip = frenchGreatest;
            aiCinematicSource.Play();
        }
        else if (dropDownContent == LanguageOptions.Mandarin)
        {
            textDisplayed.text = LanguageOptions.MandarinGreatestEnemy;
            aiCinematicSource.clip = mandarinGreatest;
            aiCinematicSource.Play();
        }
        else if (dropDownContent == LanguageOptions.Japanese)
        {
            textDisplayed.text = LanguageOptions.JapaneseGreatestEnemy;
            aiCinematicSource.clip = japaneseGreatest;
            aiCinematicSource.Play();
        }
        else if (dropDownContent == LanguageOptions.German)
        {
            textDisplayed.text = LanguageOptions.GermanGreatestEnemy;
            aiCinematicSource.clip = germanGreatest;
            aiCinematicSource.Play();
        }
    }
}
