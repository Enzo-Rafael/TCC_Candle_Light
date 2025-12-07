using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UICredits : MonoBehaviour
{
    public RawImage fader;

    [SerializeField] InputReader _inputReader;
    public UnityAction Closed;
    public RectTransform creditsText;
    public float creditsDuration;
    public Vector2 creditsStartStopPosition;
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
            creditsText.position = new Vector3(creditsText.position.x, creditsStartStopPosition.x, creditsText.position.z);
            fader.gameObject.SetActive(true);
            fader.material.SetFloat("Amount", 1);
            LeanTween.value(gameObject, SetAlpha, 1, 0, 0.5f).setOnComplete(() => { fader.gameObject.SetActive(false); });
            LeanTween.moveY(creditsText, creditsStartStopPosition.y, creditsDuration).setLoopClamp();
        }
        else
        {
            fader.gameObject.SetActive(true);
            fader.material.SetFloat("Amount", 0);
            LeanTween.value(gameObject, SetAlpha, 0, 1, 0.05f);//.setOnComplete(() => { fader.gameObject.SetActive(false); });
            LeanTween.cancel(creditsText);
            creditsText.position = new Vector3(creditsText.position.x,creditsStartStopPosition.x, creditsText.position.z);
        }

    }
    public void SetAlpha(float value)
    {
        fader.material.SetFloat("_Amount", value);

    }
}
