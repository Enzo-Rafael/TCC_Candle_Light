using UnityEngine;
using UnityEngine.Events;
public class UIPopup : MonoBehaviour
{
    public event UnityAction<bool> ConfirmationResponseAction;
    [SerializeField] InputReader _inputReader;

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

    // Gambiarra so pra buildar
    // TODO: conectar a UI
    public void Exit()
    {
        Application.Quit();
    }
}
