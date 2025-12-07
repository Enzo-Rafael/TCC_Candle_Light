using System.Collections;
using UnityEngine;

public enum CurrentMenu
{
    MAIN,
    SETTINGS,
    CREDITS,
    FEATURES,
    CHANGE_GAME_STATE,
    INTRO,
    EXIT_MENU,
    TRANSITION
}

public class MenuAnimationControl : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnimator;
    [SerializeField]
    private GameObject introHolder;
    [SerializeField]
    private GameObject fadersHolder;
    public GameObject flashHolder;

    public UIMainMenu mainMenu;
    public UISettingsController settingsScreen;
    public UICredits creditsScreen;
    public UIFeatures featuresScreen;
    public UIPopup exitPopup;
    public BtnSave btnSave;

    public bool canSkipIntro;

    private CurrentMenu currentMenu = CurrentMenu.INTRO;

#region Base Functions

    private void Start()
    {
        if(UISplashControl.Instance.currentState == SplashState.EXIT)
        {
            menuAnimator.SetTrigger("GoToMenu");
            CloseIntro();
        }
        if (UISplashControl.Instance.currentState == SplashState.END)
        {
            menuAnimator.SetTrigger("GoToMenu");
            CloseIntro();
            mainMenu.StopTween();
            CreditsTransition();
        }
    }
    public void CloseIntro()
    {
        introHolder.SetActive(false);
        mainMenu.AnimateMenuShow(true);
        canSkipIntro = false;
        currentMenu = CurrentMenu.MAIN;
        UISplashControl.Instance.SetState();
        
    }

    public void Update()
    {
        if (Input.anyKeyDown && currentMenu == CurrentMenu.INTRO && canSkipIntro)
        {
            menuAnimator.SetTrigger("GoToMenu");
            CloseIntro();
        }
    }

    public void CallExitGame()
    {
        AudioManager.Instance.PlaySound("UI_Cancel");
        currentMenu = CurrentMenu.CHANGE_GAME_STATE;
        mainMenu.AnimateMenuShow(false);
        menuAnimator.SetTrigger("GoToExit");

    }
    public void ExitGame()
    {
        Debug.Log("Goodbye!");
        Application.Quit();

    }
    public void CallNewGame()
    {
        currentMenu = CurrentMenu.CHANGE_GAME_STATE;
        mainMenu.AnimateMenuShow(false);
        AudioManager.Instance.StopSound("Music_MenuMusic");
        AudioManager.Instance.PlaySound("UI_GameStart");
        menuAnimator.SetTrigger("GoToNew");
    }
    public void CallContinueGame()
    {
        currentMenu = CurrentMenu.CHANGE_GAME_STATE;
        mainMenu.AnimateMenuShow(false);
        AudioManager.Instance.StopSound("Music_MenuMusic");
        AudioManager.Instance.PlaySound("UI_GameStart");
        menuAnimator.SetTrigger("GoToContinue");
    }
    public void StartNewGame()
    {
        btnSave.BtnNewSave();
    }
    public void ContinueGame()
    {
        flashHolder.SetActive(false);
        btnSave.BtnContinue();
    }

#endregion
#region Transitions
    public void PopupTransition()
    {
        StartCoroutine("PopupTransitionRoutine");
    }
    
    public IEnumerator PopupTransitionRoutine()
    {
        if (currentMenu == CurrentMenu.MAIN)
        {
            //AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            mainMenu.AnimateMenuShow(false);
            yield return new WaitForSeconds(0.2f);
            mainMenu.ExitButton();
            exitPopup.AnimateShow(true);
            currentMenu = CurrentMenu.EXIT_MENU;
        }
        else if (currentMenu == CurrentMenu.EXIT_MENU) 
        {
           // AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            exitPopup.AnimateShow(false);
            yield return new WaitForSeconds(0.2f);
            exitPopup.CancelButton();
            mainMenu.AnimateMenuShow(true);
            currentMenu = CurrentMenu.MAIN;
        }
    }

    public void SettingsTransition()
    {
        StartCoroutine("SettingsAnimationRoutine");
    }
    public IEnumerator SettingsAnimationRoutine()
    {
        if(currentMenu == CurrentMenu.MAIN)
        {
           // AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            menuAnimator.SetTrigger("GoToSettings");
            mainMenu.AnimateMenuShow(false);
            yield return new WaitForSeconds(0.2f);
            mainMenu.SettingsButton();
            settingsScreen.SettingsScreenAnimate(true);
            currentMenu = CurrentMenu.SETTINGS;
        }
        else if (currentMenu == CurrentMenu.SETTINGS)
        {
          //  AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            menuAnimator.SetTrigger("GoToMenu");
            settingsScreen.SettingsScreenAnimate(false);
            yield return new WaitForSeconds(0.2f);
            settingsScreen.ClosePanel();
            mainMenu.AnimateMenuShow(true);
            currentMenu = CurrentMenu.MAIN;
        }

    }
    public void CreditsTransition()
    {
        StartCoroutine("CreditsAnimationRoutine");
    }

    public IEnumerator CreditsAnimationRoutine() 
    {
        
        if(currentMenu == CurrentMenu.MAIN)
        {
            //AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            menuAnimator.SetTrigger("GoToCredits");
            mainMenu.AnimateMenuShow(false);
            yield return new WaitForSeconds(0.5f);
            mainMenu.CreditsButton();
            creditsScreen.AnimateShow(true);
            yield return new WaitForSeconds(0.3f);
            currentMenu = CurrentMenu.CREDITS;
        }
        else if (currentMenu == CurrentMenu.CREDITS)
        {
           // AudioManager.Instance.PlaySound("UI_ChangeScreen");
            currentMenu = CurrentMenu.TRANSITION;
            creditsScreen.AnimateShow(false);
            yield return new WaitForSeconds(0.15f);
            menuAnimator.SetTrigger("GoToMenu");
            creditsScreen.ClosePanel();
            mainMenu.AnimateMenuShow(true);
            yield return new WaitForSeconds(0.3f);
            currentMenu = CurrentMenu.MAIN;

        }
    }
    public void FeaturesTransition()
    {
        StartCoroutine("FeaturesAnimationRoutine");
    }
    public IEnumerator FeaturesAnimationRoutine()
    {
        if (currentMenu == CurrentMenu.MAIN)
        {
            currentMenu = CurrentMenu.TRANSITION;

            mainMenu.AnimateMenuShow(false);
            yield return new WaitForSeconds(0.2f);
            mainMenu.FeaturesButton();
            featuresScreen.AnimateShow(true);
            currentMenu = CurrentMenu.FEATURES;
        }
        else if (currentMenu == CurrentMenu.FEATURES)
        {
            currentMenu = CurrentMenu.TRANSITION;
            featuresScreen.AnimateShow(false);
            yield return new WaitForSeconds(0.2f);
            featuresScreen.ClosePanel();
            mainMenu.AnimateMenuShow(true);
            currentMenu = CurrentMenu.MAIN;
        }
    }
#endregion

}
