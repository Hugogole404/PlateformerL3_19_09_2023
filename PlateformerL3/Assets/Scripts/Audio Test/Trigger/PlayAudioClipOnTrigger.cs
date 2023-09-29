using UnityEngine;

public class PlayAudioClipOnTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip myAudioclip1;
    [SerializeField] private float volume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAudioSource.PlayOneShot(myAudioclip1, volume);
        }
    }
}
