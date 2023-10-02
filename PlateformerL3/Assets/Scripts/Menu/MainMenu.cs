using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour/*, IPointerEnterHandler*/
{
    [SerializeField] Button _button;
    [SerializeField] TMPro.TextMeshProUGUI Text;
    [SerializeField] Image ImageFade;
    [SerializeField] GameObject FadeObj;
    [SerializeField] Vector3 _buttonOriginalSize;
    [SerializeField] Vector3 _buttonResizedSize;
    [SerializeField] Color ColorInitial;
    [SerializeField] Color ColorSelected;

    #region Load Scene
    private void LoadSceneGame()
    {
        SceneManager.LoadScene("LEVEL_ART_1_DONTTOUCH");
    }
    private void LoadSceneControls()
    {
        SceneManager.LoadScene("Controls");
    }
    private void LoadSceneGameStartScene()
    {
        SceneManager.LoadScene("Start_Scene");
    }
    private void LoadSceneOptions()
    {
        SceneManager.LoadScene("Options");
    }
    private void LoadSceneCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    private void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Transform
    private void Transform()
    {
        _button.transform.DOComplete();
        _button.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.3f, 3, 0.3f);
        _button.transform.DOComplete();
        FadeObj.SetActive(true);
    }

    public void OnClickPlayMenu()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneGameStartScene);
    }
    public void OnClickControls()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneControls);
    }
    public void OnClickPlay()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneGame);
    }
    public void OnClickOptions()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneOptions);
    }
    public void OnClickPlayCredits()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneCredits);
    }
    public void OnClickMenu()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(LoadSceneMenu);
    }
    public void OnClickQuit()
    {
        Transform();
        ImageFade.DOFade(1, 0.8f).OnComplete(QuitGame);
    }
    #endregion

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

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    _button.transform.DOScale(_buttonResizedSize, 0.2f);
    //}
}