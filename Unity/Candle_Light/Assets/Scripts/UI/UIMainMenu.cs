using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject exitPanel;

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {

    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReturnSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ReturnCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void OpenExit()
    {
        exitPanel.SetActive(true);
    }

    public void ReturnExit()
    {
        exitPanel.SetActive(false);
    }
}
