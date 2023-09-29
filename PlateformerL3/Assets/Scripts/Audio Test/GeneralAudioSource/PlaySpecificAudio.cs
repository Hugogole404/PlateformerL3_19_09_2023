using UnityEngine;

public class PlaySpecificAudio : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip myAudioClip1;
    [SerializeField] private float volume = 1.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAudioSource.PlayOneShot(myAudioClip1, volume);
        }
    }
}
