using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFeatures : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    public UnityAction Closed;
    public RawImage shutter;
    void OnEnable(){
        _inputReader.MenuCloseEvent += ClosePanel;
    }
	private void OnDisable(){
        _inputReader.MenuCloseEvent -= ClosePanel;
    }
    public void ClosePanel(){
        Closed.Invoke();
    }
    public void AnimateShow(bool value)
    {
        if (value)
        {
            shutter.gameObject.SetActive(true);
            shutter.material.SetFloat("Amount", 1);
            LeanTween.value(gameObject, SetAlpha, 1, 0, 0.5f).setOnComplete(() => { shutter.gameObject.SetActive(false); });
        }
        else
        {
            shutter.material.SetFloat("Amount", 0);
            LeanTween.value(gameObject, SetAlpha, 0, 1, 0.05f);//.setOnComplete(() => { fader.gameObject.SetActive(false); });
        }
    }
    public void SetAlpha(float value)
    {
        shutter.material.SetFloat("_Amount", value);

    }
}
