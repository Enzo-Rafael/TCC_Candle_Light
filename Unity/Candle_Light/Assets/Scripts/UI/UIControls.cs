using UnityEngine;
using UnityEngine.Events;

public class UIControls : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    public UnityAction Closed;
    private void OnEnable()
    {
        _inputReader.DisableAllInput();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        _inputReader.EnableGameplayInput();  
    }
    public void ClosePanel()
    {
        Time.timeScale = 1;
        Closed.Invoke();
    }
}
