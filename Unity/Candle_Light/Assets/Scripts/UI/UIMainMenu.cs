using UnityEngine;
using UnityEngine.Events;


public class UIMainMenu : MonoBehaviour
{
    public UnityAction NewGameButtonAction;
    public UnityAction ContinueButtonAction;
    public UnityAction SettingsButtonAction;
    public UnityAction CreditsButtonAction;
    public UnityAction ExitButtonAction;
    public UnityAction FeaturesButtonAction;

    public void ContinueButton()
    {
        ContinueButtonAction.Invoke();
    }
    public void NewGameButton()
    {
        NewGameButtonAction.Invoke();
    }
    public void SettingsButton()
    {
        SettingsButtonAction.Invoke();
    }
    public void CreditsButton()
    {
        CreditsButtonAction.Invoke();
    }
    public void FeaturesButton()
    {
        FeaturesButtonAction.Invoke();
    }
    public void ExitButton()
    {
        ExitButtonAction.Invoke();
    }
}
