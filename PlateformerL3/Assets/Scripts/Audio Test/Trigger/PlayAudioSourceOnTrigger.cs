using UnityEngine;

public class PlayAudioSourceOnTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAudioSource.Play();
        }
    }
}
