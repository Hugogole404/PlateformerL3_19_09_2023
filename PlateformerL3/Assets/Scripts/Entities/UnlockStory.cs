using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStory : MonoBehaviour
{
    public CapsuleCollider2D PlayerDetection;
    public PlayerController playerController;
    public GameObject Story;
    public bool _isActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision == PlayerDetection))
        {
            _isActive = true;
            Story.SetActive(_isActive);
            playerController._isPaused = true;
        }
    }

    void AvoidStory()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _isActive)
        {
            _isActive = false;
            Story.SetActive(_isActive);
            playerController._isPaused = false;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && _isActive)
        //{
        //    _isActive = false;
        //    Story.SetActive(_isActive);
        //}

        AvoidStory();
    }
}
