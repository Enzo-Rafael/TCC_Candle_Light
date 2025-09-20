using System.Collections;
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
        StartCoroutine("ContinueRoutine");
    }
    public IEnumerator ContinueRoutine()
    {

        AudioManager.Instance.PlaySound("UI_Confirm");
        yield return new WaitForSeconds(1.5f);
        ContinueButtonAction.Invoke();
    }
    public void NewGameButton()
    {
        StartCoroutine("StartGameRoutine");
    }
    public IEnumerator StartGameRoutine()
    {
        AudioManager.Instance.StopSound("Music_MainMenuMusic");
        AudioManager.Instance.PlaySound("UI_GameStart");
        yield return new WaitForSeconds(5);
        NewGameButtonAction.Invoke();

    }
    public void SettingsButton()
    {
        AudioManager.Instance.PlaySound("UI_ChangeScreen");
        SettingsButtonAction.Invoke();

    }
    public void CreditsButton()
    {
        AudioManager.Instance.PlaySound("UI_ChangeScreen");
        CreditsButtonAction.Invoke();
    }
    public void FeaturesButton()
    {
        AudioManager.Instance.PlaySound("UI_ChangeScreen");
        FeaturesButtonAction.Invoke();
    }
    public void ExitButton()
    {
        AudioManager.Instance.PlaySound("UI_Cancel");
        ExitButtonAction.Invoke();
    }
}
