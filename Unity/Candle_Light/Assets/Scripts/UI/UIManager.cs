using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPause _pausePainel;
    [SerializeField] private UIControls _controlsPainel;
    [SerializeField] private InputReader _inputReader = default;

    void Start()
    {
        //_inputReader.EnableGameplayInput();
    }

    private void OnEnable()
    {
        _inputReader.MenuCloseEvent += OpenPause;
        _controlsPainel.Closed += CloseControls;
        _controlsPainel.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        _inputReader.MenuCloseEvent -= OpenPause;
    }
    private void OpenPause()
    {
        _inputReader.MenuCloseEvent -= OpenPause;
        Time.timeScale = 0;
        _pausePainel.ResumedAction += ClosePause;
        _pausePainel.BackToMenuAction += OpenMenu;
        _pausePainel.gameObject.SetActive(true);
        _inputReader.EnableMenuInput();
    }
    private void ClosePause()
    {
        _inputReader.MenuCloseEvent += OpenPause;
        Time.timeScale = 1;
        _pausePainel.ResumedAction -= ClosePause;
        _pausePainel.BackToMenuAction -= OpenMenu;
        _pausePainel.gameObject.SetActive(false);
        _inputReader.EnableGameplayInput();
    }
    private void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void CloseControls(bool rightActive)
    {
        _inputReader.InputSelect(rightActive);
        _controlsPainel.Closed -= CloseControls;
        _controlsPainel.gameObject.SetActive(false);
    }
}