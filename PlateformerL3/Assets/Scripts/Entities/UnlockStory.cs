using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStory : MonoBehaviour
{
    //public CapsuleCollider2D PlayerDetection;
    public PlayerController playerController;
    public GameObject Story;
    public bool _isActive = false;
    private bool _isInObject = false;
    public GameObject obj;
    [SerializeField] AudioSource CollectSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.GetComponent<PlayerController>() != null))
        {
            CollectSound.Play();
            _isActive = true;
            Story.SetActive(_isActive);
            playerController._isPaused = true;
            playerController._walkSpeed = 0;
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && _isInObject)
    //    {
    //        Story.SetActive(!_isActive);
    //        _isInObject = false;
    //    }
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if ((collision == PlayerDetection))
    //    {
    //        _isActive = true;
    //        Story.SetActive(_isActive);
    //        playerController._isPaused = true;
    //    }
    //}

    void AvoidStory()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _isActive)
        {
            _isActive = false;
            Story.SetActive(_isActive);
            playerController._isPaused = false;
            Destroy(obj);
            playerController._walkSpeed = playerController._walkSpeedMemory;
        }
    }

    private void Update()
    {
        AvoidStory();
    }
}