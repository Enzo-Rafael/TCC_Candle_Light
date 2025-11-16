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
        vp.SetDirectAudioVolume(0, AudioManager.Instance.musicVolume*AudioManager.Instance.masterVolume);

        vp.loopPointReached += (vp) => { SceneManager.LoadScene(sceneName); };
    }
}
