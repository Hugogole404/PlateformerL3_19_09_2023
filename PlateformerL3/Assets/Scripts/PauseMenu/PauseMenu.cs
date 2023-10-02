using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    [SerializeField] Button _button;
    [SerializeField] TMPro.TextMeshProUGUI Text;
    [SerializeField] Vector3 _buttonOriginalSize;
    [SerializeField] Vector3 _buttonResizedSize;
    [SerializeField] Color ColorInitial;
    [SerializeField] Color ColorSelected;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnPointerEnterButton()
    {
        _button.transform.DOComplete();
        _button.transform.DOScale(_buttonResizedSize, 0.2f);
    }
    public void OnPointerExitButton()
    {
        _button.transform.DOComplete();
        _button.transform.DOScale(_buttonOriginalSize, 0.2f);
    }
}
