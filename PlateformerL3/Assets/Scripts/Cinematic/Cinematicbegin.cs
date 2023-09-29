using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cinematicbegin : MonoBehaviour
{
    public GameObject DialogSystem;
    public float _timer;
    
    void Start()
    {
        DialogSystem.SetActive(false);
    }

    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            DialogSystem.SetActive(true);
        }
    }
}
