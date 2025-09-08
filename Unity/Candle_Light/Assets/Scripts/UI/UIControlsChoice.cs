using UnityEngine;
using UnityEngine.Events;

public class UIControlsChoice : MonoBehaviour
{

    public UnityAction RightButton;
    public UnityAction LeftButton;

    public void ButtonGhostRight(){
        RightButton.Invoke();
    }
    public void ButtonGhostLeft(){
        LeftButton.Invoke();
    }
}
