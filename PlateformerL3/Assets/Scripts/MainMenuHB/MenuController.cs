using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace MenuControllerSystem
{

    public class MenuController : MonoBehaviour
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

        [Header("Brightness Setting")]
        [SerializeField] private Brightness brightnessEffect = null;
        [SerializeField] private UnityEngine.UI.Slider brightnessSlider = null;
        [SerializeField] private TMP_Text brightnessTextValue = null;

        [Header("Sound Settings")]
        [SerializeField] private TMP_Text soundTextValue = null;
        [SerializeField] private UnityEngine.UI.Slider soundSlider = null;

        [Header("Music Settings")]
        [SerializeField] private TMP_Text musicTextValue = null;
        [SerializeField] private UnityEngine.UI.Slider musicSlider = null;

        [Header("Resolution Dropdowns")]
        public TMP_Dropdown resolutionDropdown;
        Resolution[] resolutions;

        [Header("Levels To Load")]
        public string _newGameButtonLevel;
  

        [Space(10)]
        [SerializeField] private UnityEngine.UI.Toggle fullScreenToggle;

        //commencer,quitter le jeu & audioSource click
        public void LoadGame()
        {
            SceneManager.LoadScene(_newGameButtonLevel);
        }

        public void QuitButton()
        {
            Application.Quit();
        }
        private void ClickSound()
        {
            GetComponent<AudioSource>().Play();
        }

        //Son

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

        //gestion graphique
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

        public void ResetButton(string OptionsMenu)
        {
            if (OptionsMenu == "Options")
            {
                //brightnessEffect.brightness = defaultBrightness;
                brightnessSlider.value = defaultBrightness;
                brightnessTextValue.text = defaultBrightness.ToString("0.0");

                fullScreenToggle.isOn = false;
                Screen.fullScreen = false;

                Resolution currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
                resolutionDropdown.value = resolutions.Length;
                GraphicsApply();
            
                AudioListener.volume = defaultSound;
                soundSlider.value = defaultSound;
                soundTextValue.text = defaultSound.ToString("0.0");
                SoundApply();

                AudioListener.volume = defaultMusic;
                soundSlider.value = defaultMusic;
                soundTextValue.text = defaultMusic.ToString("0.0");
                MusicApply();

            }

        }


    }
}
