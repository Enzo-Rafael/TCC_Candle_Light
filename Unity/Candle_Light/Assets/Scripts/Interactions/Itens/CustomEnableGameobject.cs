using UnityEngine;

public class CustomEnableGameobject : MonoBehaviour, ICodeCustom
{
    [SerializeField] GameObject objectReference;
    [SerializeField] private bool onOff;
    public void CustomBaseAction(object additionalInformation){
        if (objectReference != null) objectReference.SetActive(onOff);
    }
}

