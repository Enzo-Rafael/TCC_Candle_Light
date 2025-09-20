using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    
    public enum SoundChannel { SFX, MUSIC }

    private AudioSource audioSource;
    public bool soundEnabled;
    public string audioName;
    public float volumeMultiplyer = 1.0f;
    public SoundChannel channel;
    public bool isHeardByGhost;
    public bool isHeardByGirl;
    public bool isAudioLocal;
    private float localVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        SetPan();
        SetAudioLocal();
        RegisterAudio();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void PlaySound()
    {
        if (!soundEnabled) 
        { 
            return;
        }
        if (soundEnabled) 
        { 
            audioSource.Play();
        }
    }

    public void PlaySound(Vector3 pos)
    {
        if (!soundEnabled)
        {
            return;
        }
        transform.position = pos;
        if (soundEnabled)
        {
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
        transform.position = Vector3.zero;
    }


    public void SetVolume( float value)
    {
        audioSource.volume = value * volumeMultiplyer;
        localVolume = value * volumeMultiplyer;
    }


    public void SetPan()
    {
        float newPan;
        if (isHeardByGhost && !isHeardByGirl) 
        {
            newPan = 1.0f;
        }
        else if (!isHeardByGhost && isHeardByGirl)
        {
            newPan = -1.0f;
        }
        else
        {
            newPan = 0.0f;
        }
        audioSource.panStereo = newPan;
    }

    public void SetAudioLocal()
    {
        audioSource.spatialBlend = isAudioLocal ? 1.0f : 0.0f;
    }

    public void RegisterAudio()
    {
        AudioManager.Instance.AddPlayerToList(audioName, this);
    }

    public void ScaleVolume(float volume)
    {
        audioSource.volume = localVolume * volume;
    }
}
