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
        SaveLoad.Instance.SaveConfig();
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
        senceValue = x;
        SaveLoad.Instance.senceRef = senceValue;
        //_inputReader.ChangeScale(senceValue);
    }

    public void LoadConfig()
    {
        path = Application.dataPath + "/saveConfig.txt";
        if (File.Exists(path))
        {
            string s = File.ReadAllText(path);
            SceneConfigData data = JsonUtility.FromJson<SceneConfigData>(s);
            //Sliders
            musicSlider.value = data.configData.audioMusic;
            sfxSlider.value = data.configData.audioSfx;
            masterSlider.value = data.configData.audioMaster;
            senceSlider.value = data.configData.senceRef;
            brightSlider.value = data.configData.brightRef;
            //AudioManager
            AudioManager.Instance.SetMaster((float)data.configData.audioMaster * 5);
            AudioManager.Instance.SetSfx((float)data.configData.audioSfx * 5);
            AudioManager.Instance.SetMusic((float)data.configData.audioMusic * 5);
            SenceLoad(data.configData.senceRef);
            
            Debug.Log("Load Config");
        }
    }
    
    public void SaveConfig()
    {
        float musicSliderValue = musicSlider.value;
        float sfxSliderValue = sfxSlider.value;
        float masterSliderValue = masterSlider.value;
        SaveLoad.Instance.SetAudioMusic((int)musicSliderValue);
        SaveLoad.Instance.SetAudioSfx((int)sfxSliderValue);
        SaveLoad.Instance.SetAudioMaster((int)masterSliderValue);
        SaveLoad.Instance.senceRef = senceValue;
        SaveLoad.Instance.SaveConfig();
        Debug.Log("Save Config");
    }
}
