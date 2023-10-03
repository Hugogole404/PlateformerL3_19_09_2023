using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Sound.Play();
            collision.transform.position = collision.gameObject.GetComponent<PlayerController>()._spawnPoint.transform.position;
        }
    }

}