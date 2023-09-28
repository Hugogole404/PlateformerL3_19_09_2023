using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PauseMenuSystem
{
    public class PauseController : MonoBehaviour
    {
        [Header("Variables to save")]
        private float _soundLevel;
        private float _musicLevel;
        private bool _isFullScreen;
        private float _brightnessLevel;

        [Header("Default Menu Values")]
        [SerializeField] private float defaultBrightness = 1;
        [SerializeField] private float defaultSound = 0.5f;
        [SerializeField] private float defaultMusic = 0.5f;

        [Header("Confirmation Object")]
        [SerializeField] private GameObject confirmationPrompt = null;

        /*[Header("Controller Sensitivity")]
        [SerializeField] private TMP_Text controllerSenText = null;
        [SerializeField] private Slider controllerSenSlider = null;*/

        [Header("Brightness Setting")]
        [SerializeField] private Brightness brightnessEffect = null;
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private TMP_Text brightnessText = null;

        [Header("Sound Settings")]
        [SerializeField] private TMP_Text soundTextValue = null;
        [SerializeField] private Slider soundSlider = null;

        [Header("Music Settings")]
        [SerializeField] private TMP_Text musicTextValue = null;
        [SerializeField] private Slider musicSlider = null;

        [Header("Resolution Dropdowns")]
        public TMP_Dropdown resolutionDropdown;
        Resolution[] resolutions;

        [SerializeField] private Toggle fullScreenToggle;

        public void ExitButton()
        {
            Application.Quit();
        }
        public void SetSoundVolume(float soundVolume)
        {
            AudioListener.volume = soundVolume;
            soundTextValue.text = soundVolume.ToString("0.0");
        }

        public void SetMusicVolume(float musicVolume)
        {
            AudioListener.volume = musicVolume;
            soundTextValue.text = musicVolume.ToString("0.0");
        }
        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetBrightness(float brightness)
        {
            _brightnessLevel = brightness;
            brightnessText.text = brightness.ToString("0.0");
        }

        public void SetFullScreen(bool isFullscreen)
        {
            _isFullScreen = isFullscreen;
        }


        public void GraphicsApply()
        {
            PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
            brightnessEffect.brightness = _brightnessLevel;

            PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
            Screen.fullScreen = _isFullScreen;

            StartCoroutine(ConfirmationBox());
        }

        public IEnumerator ConfirmationBox()
        {
            confirmationPrompt.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationPrompt.SetActive(false);
        }

        private void ClickSound()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
