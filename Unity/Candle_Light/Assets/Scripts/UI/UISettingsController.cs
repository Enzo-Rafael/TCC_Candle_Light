using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UISettingsController : MonoBehaviour
{


    [SerializeField] InputReader _inputReader;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public UnityAction Closed;
    public Slider brightSlider;
    public Slider senceSlider;
    private string path;
    //---------BackGroundVariables------
    [NonSerialized] public float brightnessValue;
    [NonSerialized] public int senceValue;

    void Start()
    {
        //masterSlider.value = AudioManager.Instance.masterVolume * 5;
        //sfxSlider.value = AudioManager.Instance.sfxVolume * 5;
        //musicSlider.value = AudioManager.Instance.musicVolume * 5;
        //brightSlider.value = 0.5f;
        //senceSlider.value = SaveLoad.Instance.senceRef;
        LoadConfig();
    }
    void OnEnable()
    {
        _inputReader.MenuCloseEvent += ClosePanel;
        //masterSlider.value = AudioManager.Instance.masterVolume * 5;
        //sfxSlider.value = AudioManager.Instance.sfxVolume * 5;
        //musicSlider.value = AudioManager.Instance.musicVolume * 5;
    }
    private void OnDisable()
    {
        _inputReader.MenuCloseEvent -= ClosePanel;
    }
    public void ClosePanel()
    {
        Closed.Invoke();
    }
    public void SetMusicVolume()
    {
        float value = musicSlider.value;
        AudioManager.Instance.SetMusic(value);
        SaveLoad.Instance.SetAudioMusic((int)value);
    }
    public void SetSfxVolume()
    {
        float value = sfxSlider.value;
        AudioManager.Instance.SetSfx(value);
        AudioManager.Instance.PlaySound("UI_Confirm");
        SaveLoad.Instance.SetAudioSfx((int)value);
    }
    public void SetMasterVolume()
    {
        float value = masterSlider.value;
        AudioManager.Instance.SetMaster(value);
        SaveLoad.Instance.SetAudioMaster((int)value);
    }
    //
    public void SetBrightness()
    {
        brightnessValue = brightSlider.value;
        SaveLoad.Instance.brightRef = (int)brightnessValue;
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue, 1);

        Debug.Log(brightnessValue);
    }
    public void SetSence()
    {
        senceValue = (int)senceSlider.value;
        SenceLoad(senceValue);
    }
    public void SenceLoad(int x)
    {
        SaveLoad.Instance.senceRef = senceValue;
        _inputReader.ChangeScale(senceValue);
    }
    
    public void LoadConfig()
    {
        path = Application.dataPath + "/save.txt";
        if (File.Exists(path))
        {
            string s = File.ReadAllText(path);
            SceneData data = JsonUtility.FromJson<SceneData>(s);
            AudioManager.Instance.SetMaster(data.configData.audioMaster);
            AudioManager.Instance.SetSfx(data.configData.audioSfx);
            AudioManager.Instance.SetMusic(data.configData.audioMusic);
            SenceLoad(data.configData.senceRef);
        }
    }
}
