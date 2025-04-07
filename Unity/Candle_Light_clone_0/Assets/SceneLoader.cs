using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerenciador de carregamento de cenas
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Carrega uma cena de nome sceneName atraves do SceneManager. E feio mas funciona
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
