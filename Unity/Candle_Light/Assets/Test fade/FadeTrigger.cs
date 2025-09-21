using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTrigger : MonoBehaviour
{
    public static FadeTrigger instance;
    public CanvasGroup canvasGroup;

    private bool fadeIn = false;

    public float timeToFade;

    private int fadeInControl = 0;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                    if (fadeInControl == 0)
                    {
                        SaveLoad.Instance.NewSave();
                        SceneManager.LoadScene("Mansion");
                    }
                    else
                    {
                        SaveLoad.Instance.StartLoad();
                    }
                    
                }
            }
        }
    }

    public void FadeIn(int NewGame)
    {

        fadeIn = true;
    }
}
