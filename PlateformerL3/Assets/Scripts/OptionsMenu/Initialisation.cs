using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Initialisation : MonoBehaviour
{

    [Header("General Settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private OptionsController optionsController;

    [Header("Sound Setting")]
    [SerializeField] private TMP_Text soundTextValue = null;
    [SerializeField] private Slider soundSlider = null;

    [Header("Music Setting")]
    [SerializeField] private TMP_Text musicTextValue = null;
    [SerializeField] private Slider musicSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    
    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("soundVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("soundVolume");

                soundTextValue.text = localVolume.ToString();
                soundSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                optionsController.ResetButton("Sound");
            }

            if (PlayerPrefs.HasKey("musicVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("musicVolume");

                soundTextValue.text = localVolume.ToString();
                soundSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                optionsController.ResetButton("Music");
            }

            if (PlayerPrefs.HasKey("masterFullscreen"))
            {
                int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

                if (localFullscreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }

                if (PlayerPrefs.HasKey("masterBrightness"))
                {
                    float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                    brightnessTextValue.text = localBrightness.ToString("0.0");
                    brightnessSlider.value = localBrightness;
                }
            }
        }
    }
}
