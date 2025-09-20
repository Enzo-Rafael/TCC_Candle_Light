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
    void OnEnable(){
        _inputReader.MenuCloseEvent += ClosePanel;
        masterSlider.value = AudioManager.Instance.masterVolume * 100;
        sfxSlider.value = AudioManager.Instance.sfxVolume * 100;
        musicSlider.value = AudioManager.Instance.musicVolume * 100;
    }
	private void OnDisable(){
        _inputReader.MenuCloseEvent -= ClosePanel;
    }
    public void ClosePanel(){
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
}
