using System;
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
    //---------BackGroundVariables------
    [NonSerialized] public float brightnessValue;
    [NonSerialized] public int senceValue;

    void Start()
    {
        //masterSlider.value = AudioManager.Instance.masterVolume * 5;
        //sfxSlider.value = AudioManager.Instance.sfxVolume * 5;
        //musicSlider.value = AudioManager.Instance.musicVolume * 5;
        brightSlider.value = 0.5f;
        senceSlider.value = SaveLoad.Instance.sence;
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
    }
    public void SetSfxVolume()
    {
        float value = sfxSlider.value;
        AudioManager.Instance.SetSfx(value);
        AudioManager.Instance.PlaySound("UI_Confirm");
    }
    public void SetMasterVolume()
    {
        float value = masterSlider.value;
        AudioManager.Instance.SetMaster(value);
    }
    //
    public void SetBrightness()
    {
        brightnessValue = brightSlider.value;
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue, 1);
        Debug.Log(brightnessValue);
    }
    public void SetSence()
    {
        senceValue = (int)senceSlider.value;
        SaveLoad.Instance.sence = senceValue;
        _inputReader.ChangeScale(senceValue);
    }
}
