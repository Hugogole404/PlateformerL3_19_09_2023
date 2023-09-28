using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("DeathZone")]
    [SerializeField] PlayerController PlayerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = PlayerController._spawnPoint.transform.position;
    }
}
