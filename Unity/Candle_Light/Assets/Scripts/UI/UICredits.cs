using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UICredits : MonoBehaviour
{
    public RawImage fader;

    [SerializeField] InputReader _inputReader;
    public UnityAction Closed;
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
            fader.gameObject.SetActive(true);
            fader.material.SetFloat("Amount", 1);
            LeanTween.value(gameObject, SetAlpha, 1, 0, 0.5f).setOnComplete(() => { fader.gameObject.SetActive(false); });
        }
        else
        {
            fader.gameObject.SetActive(true);
            fader.material.SetFloat("Amount", 0);
            LeanTween.value(gameObject, SetAlpha, 0, 1, 0.05f);//.setOnComplete(() => { fader.gameObject.SetActive(false); });
        }
    }
    public void SetAlpha(float value)
    {
        fader.material.SetFloat("_Amount", value);

    }
}
