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
    public GameObject animationContainer;
    

    public void AnimateMenuShow(bool value)
    {
        if (value)
        {
            LeanTween.moveX(animationContainer.GetComponent<RectTransform>(), 0, 0.15f);
            LeanTween.moveY(FeatureButtonRef.gameObject.GetComponent<RectTransform>(), 30, 0.5f).setDelay(0.15f);
            LeanTween.moveY(ExitButtonRef.gameObject.GetComponent<RectTransform>(), 30, 0.5f).setDelay(0.15f);
        }
        else
        {
            LeanTween.moveY(FeatureButtonRef.gameObject.GetComponent<RectTransform>(), -110, 0.15f);
            LeanTween.moveY(ExitButtonRef.gameObject.GetComponent<RectTransform>(), -120, 0.15f);
            LeanTween.moveX(animationContainer.GetComponent<RectTransform>(), -900, 0.5f).setDelay(0.15f);
        }
    }
    public void StopTween()
    {
        LeanTween.cancel(animationContainer.GetComponent<RectTransform>());
        LeanTween.cancel(ExitButtonRef.GetComponent<RectTransform>());
        LeanTween.cancel(FeatureButtonRef.GetComponent<RectTransform>());
    }

    public void ContinueButton()
    {
        ContinueButtonRef.interactable = false;
        NewGameButtonRef.interactable = false;
        SettingsButtonRef.interactable = false;
        CreditsButtonRef.interactable = false;
        ExitButtonRef.interactable = false;
        FeatureButtonRef.interactable = false;
        ContinueButtonAction.Invoke();
    }
    public IEnumerator ContinueRoutine()
    {

        AudioManager.Instance.PlaySound("UI_Confirm");
        yield return new WaitForSeconds(1.5f);
    }
    public void NewGameButton()
    {
        ContinueButtonRef.interactable = false;
        NewGameButtonRef.interactable = false;
        SettingsButtonRef.interactable = false;
        CreditsButtonRef.interactable = false;
        ExitButtonRef.interactable = false;
        FeatureButtonRef.interactable = false;
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
        AudioManager.Instance.PlaySound("UI_ChangeScreen");
        ExitButtonAction.Invoke();
    }
}
