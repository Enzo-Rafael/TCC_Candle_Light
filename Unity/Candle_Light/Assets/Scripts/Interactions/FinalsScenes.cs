using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalsScenes : MonoBehaviour
{
    [SerializeField]
    private string finalSceneName;
    public void OnTriggerDetected(bool entered, GameObject gameObject){
        if (entered){
            Analytics.Instance.Enviar();
            SceneManager.LoadScene(finalSceneName);
        }
    }
}
