using UnityEngine;
using UnityEngine.Events;

public class UISettingsController : MonoBehaviour
{

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
}
