using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class OptionsController : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private TMP_Text soundTextValue = null;
    [SerializeField] private Slider soundSlider = null;

    [Header("Music Settings")]
    [SerializeField] private TMP_Text musicTextValue = null;
    [SerializeField] private Slider musicSlider = null;

    [Header("Confirmation Prompt")]
    [SerializeField] private GameObject confirmationPrompt = null;

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

}
