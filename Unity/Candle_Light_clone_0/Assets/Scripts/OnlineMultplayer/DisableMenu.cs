using UnityEngine;

public class DisableMenu : MonoBehaviour
{
    private void OnDisable()
    {
         Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
