using Unity.Cinemachine;
using UnityEngine;

public class FadeOutcontroller : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private bool fadeOut = false;

    public float timeToFade;
    private GameObject p1; // Player 1
    private GameObject p2; // Player 2
    private GameObject cP2; // Camera do player 2


    void Awake()
    {
        canvasGroup.alpha = 1;
        FadeOut();
    }
    void Start()
    {
        p1 = GameObject.Find("Player1");
        p2 = GameObject.Find("Player2");
        cP2 = GameObject.Find("P2 Follow Cam (1)");
        p1.GetComponent<PlayerOneScript>().enabled = false;
        p2.GetComponent<PlayerTwoScript>().enabled = false;
        //cP2.GetComponent<CinemachineInputAxisController>().enabled = false;
    }

    void Update()
    {
        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    
                    p1.GetComponent<PlayerOneScript>().enabled = true;
                    p2.GetComponent<PlayerTwoScript>().enabled = true;
                    //cP2.GetComponent<CinemachineInputAxisController>().enabled = true;
                    fadeOut = false;
                }
            }
        }
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
