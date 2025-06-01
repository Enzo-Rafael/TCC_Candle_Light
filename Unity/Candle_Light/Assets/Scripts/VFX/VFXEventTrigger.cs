using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VFXEventTrigger : MonoBehaviour
{
    private VisualEffect vfx;

    //[SerializeField]
    //private string eventName;

    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }

    public void TriggerEvent(string eventName)
    {
        vfx.SendEvent(eventName);
    }
}
