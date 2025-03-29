using UnityEngine;
using UnityEngine.Events;

public class UIPause : MonoBehaviour
{
    public UnityAction ResumedAction;
    public UnityAction BackToMenuAction;

    [SerializeField] private InputReader _inputReader = default;
    void OnEnable(){
        _inputReader.MenuCloseEvent += Resume;
    }
    void OnDisable(){
        _inputReader.MenuCloseEvent -= Resume;
    }
    public void Resume(){
        ResumedAction.Invoke();
    }
    public void BackToMenu(){
        BackToMenuAction.Invoke();
    }
}
