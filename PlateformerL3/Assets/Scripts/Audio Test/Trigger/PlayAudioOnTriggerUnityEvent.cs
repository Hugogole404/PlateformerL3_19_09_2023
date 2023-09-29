using UnityEngine;
using UnityEngine.Events;

public class PlayAudioOnTriggerUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent myAudioEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAudioEvent.Invoke();
        }
    }
}
