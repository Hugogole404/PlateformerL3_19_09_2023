using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _buttonMenu;
    [SerializeField] TMPro.TextMeshProUGUI Text;
    [SerializeField] Image ImageFadeObject;
    [SerializeField] GameObject FadeObject;
    [SerializeField] Vector3 _buttonOriginSize;
    [SerializeField] Vector3 _buttonNewSize;
    [SerializeField] Color ColorInitial;
    [SerializeField] Color ColorSelected;

    #region Load Scene
    private void LoadMainGame()
    {
        SceneManager.LoadScene("LEVEL_ART_1_DONTTOUCH");
    }
    private void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }
    private void LoadStartCinematic()
    {
        SceneManager.LoadScene("Start_Scene");
    }
    private void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }
    private void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Transform
    private void TransformScale()
    {
        _buttonMenu.transform.DOComplete();
        _buttonMenu.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0), 0.3f, 3, 0.3f);
        _buttonMenu.transform.DOComplete();
        FadeObject.SetActive(true);
    }
    public void OnClickPlayMenu()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadStartCinematic);
    }
    public void OnClickControls()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadControls);
    }
    public void OnClickPlay()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadMainGame);
    }
    public void OnClickOptions()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadOptions);
    }
    public void OnClickPlayCredits()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadCredits);
    }
    public void OnClickMenu()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(LoadMenu);
    }
    public void OnClickQuit()
    {
        TransformScale();
        ImageFadeObject.DOFade(1, 0.8f).OnComplete(ExitGame);
    }
    #endregion

    public void OnPointerEnterButton()
    {
        _buttonMenu.transform.DOComplete();
        _buttonMenu.transform.DOScale(_buttonNewSize, 0.2f);
    }
    public void OnPointerExitButton()
    {
        _buttonMenu.transform.DOComplete();
        _buttonMenu.transform.DOScale(_buttonOriginSize, 0.2f);
    }
}