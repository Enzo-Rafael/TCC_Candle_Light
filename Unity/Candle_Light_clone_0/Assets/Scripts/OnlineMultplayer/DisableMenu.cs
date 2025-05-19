using UnityEngine;

public class DisableMenu : MonoBehaviour
{
     private void Start()
    {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true; 

    }
       
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
