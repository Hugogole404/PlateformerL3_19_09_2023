using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStory : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject Story;
    public bool _isActive = false;
    private bool _isInObject = false;
    public GameObject obj;
    [SerializeField] AudioSource CollectSound;
    public NotesCount CounterNotes;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.GetComponent<PlayerController>() != null))
        {
            playerController._spawnPoint.transform.position = transform.position;
            _isActive = true;
            Story.SetActive(_isActive);
            playerController._isPaused = true;
            playerController._walkSpeed = 0;
        }
    }

    void AvoidStory()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _isActive)
        {
            _isActive = false;
            Story.SetActive(_isActive);
            playerController._isPaused = false;
            CollectSound.Play();
            Destroy(obj);
            playerController._walkSpeed = playerController._walkSpeedMemory;
            CounterNotes.CountNotes += 1;
        }
    }

    private void Update()
    {
        AvoidStory();
    }
}