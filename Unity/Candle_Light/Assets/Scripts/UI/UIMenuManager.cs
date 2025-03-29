using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMenuManager : MonoBehaviour
{
    [SerializeField] UIMainMenu _menuPainel = default;
    [SerializeField] UISettingsController _settingPanel = default;
    [SerializeField] UICredits _creditsPanel = default;
    [SerializeField] UIPopup _popupPanel = default;


    [SerializeField] InputReader _inputReader = default;


    void Start(){
        _inputReader.EnableMenuInput();
        SetMenu();       
    }

    private void SetMenu(){
        _menuPainel.NewGameButtonAction += StartNewGame;
        _menuPainel.SettingsButtonAction += OpenSettings;
        _menuPainel.CreditsButtonAction += OpenCredits;
        _menuPainel.ExitButtonAction += ShowExitConfirmationPopup;
    }

    private void StartNewGame(){
        SceneManager.LoadScene("SampleScene");
    }
    private void OpenSettings(){
        _settingPanel.gameObject.SetActive(true);
        _settingPanel.Closed += CloseSettings;
    }
    private void CloseSettings(){
        _settingPanel.Closed -= CloseSettings;
        _settingPanel.gameObject.SetActive(false);
    }
    private void OpenCredits(){
        _creditsPanel.gameObject.SetActive(true);
        _creditsPanel.Closed += CloseCredits;
    }
    private void CloseCredits(){
        _creditsPanel.Closed -= CloseCredits;
        _creditsPanel.gameObject.SetActive(false);
    }
	public void ShowExitConfirmationPopup(){
		_popupPanel.ConfirmationResponseAction += HideExitConfirmationPopup;
		_popupPanel.gameObject.SetActive(true);
	}
    void HideExitConfirmationPopup(bool quitConfirmed){
		_popupPanel.ConfirmationResponseAction -= HideExitConfirmationPopup;
		_popupPanel.gameObject.SetActive(false);
		if (quitConfirmed)Application.Quit();
	}
}
