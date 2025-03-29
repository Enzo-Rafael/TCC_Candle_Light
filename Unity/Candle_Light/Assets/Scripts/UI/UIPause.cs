using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIPause : MonoBehaviour
{
    public GameObject pausePanel;
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void Close()
    {
        pausePanel.SetActive(false);
    }


}
