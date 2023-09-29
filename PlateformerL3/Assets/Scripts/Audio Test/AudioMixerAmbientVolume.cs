using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerAmbientVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;

    [Range(-80, 20)][SerializeField] private float ambientVolume = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAudioMixer.SetFloat("AmbientVolume", ambientVolume);
        }
    }
}
