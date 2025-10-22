using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutcontroller : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [SerializeField]private bool fadeOut = false;

    public float timeToFade;
    [SerializeField]private InputReader playersImput = default; // Player 1


    void Awake()
    {

    }
    void Start()
    {
        //canvasGroup.alpha = 1;
        playersImput.DisableAllInput();
        SceneManager.sceneLoaded += FadeOut;
        AudioManager.Instance.SetMaster(0);
    }

    void Update()
    {
        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha <= 0.0f)
                {
                    Debug.Log("etrou fadeOut");
                    AudioManager.Instance.SetMaster(SaveLoad.Instance.GetAudioMaster());
                    //playersImput.EnableAllInput();
                    fadeOut = false;
                }
            }
        }
    }

    public void FadeOut(Scene scene, LoadSceneMode mode)
    {
        fadeOut = true;
    }
}
