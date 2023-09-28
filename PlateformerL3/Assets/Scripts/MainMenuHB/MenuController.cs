using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private TMP_Text soundTextValue = null;
    [SerializeField] private UnityEngine.UI.Slider soundSlider = null;

    [Header("Music Settings")]
    [SerializeField] private TMP_Text musicTextValue = null;
    [SerializeField] private UnityEngine.UI.Slider musicSlider = null;

    [Header("Confirmation Prompt")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Graphic Settings")]
    [SerializeField] private UnityEngine.UI.Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Space(10)]
    [SerializeField] private UnityEngine.UIElements.Toggle fullScreenToggle;

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
    }

    //    fullScreenToggle.isOn = false;
    //    Screen.fullScreen = false;

    //    Resolution currentResolution = Screen.currentResolution;
    //    Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
    //    resolutionDropdown.value = resolutions.Length;
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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

    public void SoundApply()
    {
        PlayerPrefs.SetFloat("soundVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }
    public void MusicApply()
    {
        PlayerPrefs.SetFloat("musicVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }

    public void SetBrigthness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void setFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SceneLDTest");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
