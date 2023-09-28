using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.transform.position = collision.gameObject.GetComponent<PlayerController>()._spawnPoint.transform.position;
        }
    }
}
