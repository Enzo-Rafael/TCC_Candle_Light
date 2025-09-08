using UnityEngine;
using UnityEngine.Events;

public class UIControls : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
 public event UnityAction<bool> Closed;
    private void OnEnable(){
        _inputReader.DisableAllInput();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable(){
        _inputReader.EnableGameplayInput();
    }
    public void ClosePanel(bool rightActive){
        Closed.Invoke(rightActive);
    }
}
