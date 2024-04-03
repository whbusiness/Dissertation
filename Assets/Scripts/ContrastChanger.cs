using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ContrastChanger : MonoBehaviour
{
    public PostProcessProfile profile;
    [NonSerialized]
    public AutoExposure exposure;
    public Slider contrastSlider, redColorSlider, greenColorSlider, blueColorSlider, satSlider;
    private IsSpeechRecogOn brightnessSaver;
    [NonSerialized]
    public ColorGrading colorGrade;
    [SerializeField]
    private Color colorFilter;
    private void Awake()
    {
        brightnessSaver = FindObjectOfType<IsSpeechRecogOn>();
        profile.TryGetSettings(out exposure);
        profile.TryGetSettings(out colorGrade);
    }

    private void Start()
    {
        redColorSlider.value = brightnessSaver.redValue;
        greenColorSlider.value = brightnessSaver.greenValue;
        blueColorSlider.value = brightnessSaver.blueValue;
        colorFilter = brightnessSaver.colorFilter;
        satSlider.value = brightnessSaver.saturation;
    }
    public void OnContrastSliderChange(float value)
    {
        value = Mathf.Clamp(contrastSlider.value, 0.01f, 1);
        exposure.keyValue.value = value;
        brightnessSaver.brightnessValue = value;
        print(value);
    }

    public void OnRedColorSlider(float value)
    {
        value = redColorSlider.value;
        colorFilter = new Color(value, colorFilter.g, colorFilter.b);
        colorGrade.colorFilter.value = colorFilter;
        brightnessSaver.colorFilter = colorFilter;
        brightnessSaver.redValue = value;
    }
    public void OnGreenColorSlider(float value)
    {
        value = greenColorSlider.value;
        colorFilter = new Color(colorFilter.r, value, colorFilter.b);
        colorGrade.colorFilter.value = colorFilter;
        brightnessSaver.colorFilter = colorFilter;
        brightnessSaver.greenValue = value;
    }
    public void OnBlueColorSlider(float value)
    {
        value = blueColorSlider.value;
        colorFilter = new Color(colorFilter.r, colorFilter.g, value);
        colorGrade.colorFilter.value = colorFilter;
        brightnessSaver.colorFilter = colorFilter;
        brightnessSaver.blueValue = value;
    }

    public void OnSatSlider(float value)
    {
        value = satSlider.value;
        colorGrade.saturation.value = value;
        brightnessSaver.saturation = value;
    }
}
