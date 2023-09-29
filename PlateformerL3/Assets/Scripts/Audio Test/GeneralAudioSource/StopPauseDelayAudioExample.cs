using UnityEngine;

public class StopPauseDelayAudioExample : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!myAudioSource.isPlaying)
            {
                //faire jouer du son, faire la cuisine, ce que tu vuex
            }
            else
            {
                myAudioSource.Pause(); //Pause l'audio et relance au point d'arrêt
                myAudioSource.Play(); //jouer l'audio après la pause
                myAudioSource.Stop();
                myAudioSource.PlayDelayed(3); //Joue l'audio source 3 secondes après que la ligne est appelée
            }
        }
    }
}
