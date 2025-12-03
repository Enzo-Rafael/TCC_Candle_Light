using UnityEngine;
using UnityEngine.Events;
public class UIPopup : MonoBehaviour
{
    public event UnityAction<bool> ConfirmationResponseAction;
    [SerializeField] InputReader _inputReader;

    public MenuAnimationControl animationControl;
    public RectTransform popupBox;

    void OnEnable(){
        _inputReader.MenuCloseEvent += CancelButton;
    }
	private void OnDisable(){
        _inputReader.MenuCloseEvent -= CancelButton;
    }
    public void CancelButton(){
        ConfirmationResponseAction.Invoke(false);
    }
    public void ConfirmButton(){
        ConfirmationResponseAction.Invoke(true);
    }
    public void AnimateShow(bool value, bool isExit = false)
    {
        if (value)
        {
            LeanTween.moveY(popupBox, 0, 0.1f);
        }
        else
        {
            LeanTween.moveY(popupBox, -1100, 0.1f).setOnComplete(() => { if (isExit) { CancelButton(); animationControl.CallExitGame(); } });
        }

    }
    public void AnswerPopup(bool isYes)
    {
        if (isYes) 
        {
            AnimateShow(false, true);
            //animationControl.CallExitGame();
        }
        else
        {
            animationControl.PopupTransition();
        }
    }
}
