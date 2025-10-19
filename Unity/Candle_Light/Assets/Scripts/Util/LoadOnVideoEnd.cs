using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadOnVideoEnd : MonoBehaviour
{

    [SerializeField] private string sceneName;
    private VideoPlayer vp;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();

        vp.loopPointReached += (vp) => { SceneManager.LoadScene(sceneName); };
    }
}
