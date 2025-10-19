using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    [SerializeField] private AudioPlayer[] audios;

    public void PlayAudio(int index)
    {
        audios[index].PlaySound();
    }
}
