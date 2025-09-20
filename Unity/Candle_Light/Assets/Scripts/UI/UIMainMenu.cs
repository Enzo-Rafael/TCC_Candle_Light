using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIMainMenu : MonoBehaviour
{
    public UnityAction NewGameButtonAction;
    public UnityAction ContinueButtonAction;
    public UnityAction SettingsButtonAction;
    public UnityAction CreditsButtonAction;
    public UnityAction ExitButtonAction;
    public UnityAction FeaturesButtonAction;
    public Button ContinueButtonRef;
    public Button NewGameButtonRef;
    public Button SettingsButtonRef;
    public Button CreditsButtonRef;
    public Button ExitButtonRef;
    public Button FeatureButtonRef;

    public void ContinueButton()
    {
        ContinueButtonRef.interactable = false;
        NewGameButtonRef.interactable = false;
        SettingsButtonRef.interactable = false;
        CreditsButtonRef.interactable = false;
        ExitButtonRef.interactable = false;
        FeatureButtonRef.interactable = false;
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
        ContinueButtonRef.interactable = false;
        NewGameButtonRef.interactable = false;
        SettingsButtonRef.interactable = false;
        CreditsButtonRef.interactable = false;
        ExitButtonRef.interactable = false;
        FeatureButtonRef.interactable = false;
        StartCoroutine("StartGameRoutine");
    }
    public IEnumerator StartGameRoutine()
    {
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
