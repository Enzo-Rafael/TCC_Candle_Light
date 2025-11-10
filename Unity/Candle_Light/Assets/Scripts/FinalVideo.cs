using UnityEngine;
using UnityEngine.Video;

public class FinalVideo : MonoBehaviour
{
    private VideoPlayer vp;
    void Start(){
        vp = GetComponent<VideoPlayer>();
        vp.clip = Resources.Load<VideoClip>("Video/Final" + SaveLoad.Instance.GetFinal());
        vp.Play();
    }
}
