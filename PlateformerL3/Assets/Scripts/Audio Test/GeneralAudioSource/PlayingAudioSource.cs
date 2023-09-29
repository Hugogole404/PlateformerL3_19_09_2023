using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAudioSource.Play();
        }
    }
}
