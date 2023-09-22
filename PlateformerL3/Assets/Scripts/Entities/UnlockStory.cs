using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStory : MonoBehaviour
{
    public CapsuleCollider2D PlayerDetection;
    public GameObject Story;
    public bool _isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isActive = true;
            Story.SetActive(_isActive);
        }
    }
    void AvoidStory()
    {
        if (_isActive)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isActive = false;
                Story.SetActive(_isActive);
            }
        }
    }
    private void Update()
    {
        AvoidStory();
    }
}
