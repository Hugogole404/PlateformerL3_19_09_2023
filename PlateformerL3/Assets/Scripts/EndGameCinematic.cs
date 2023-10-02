using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EndGameCinematic : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] GameObject FadeObject;
    [SerializeField] Image ImageFadeObject;

    private void LoadEndGame()
    {
        SceneManager.LoadScene("End_Scene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<PlayerController>() != null))
        {
            FadeObject.SetActive(true);
            ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadEndGame);
        }
    }
}
