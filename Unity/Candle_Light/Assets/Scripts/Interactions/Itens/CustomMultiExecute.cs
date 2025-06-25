using UnityEngine;

public class CustomMultiExecute : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    private IInteractable[] interactables;

    public void CustomBaseAction(object additionalInformation)
    {
        foreach (IInteractable interactable in interactables)
        {
            interactable.BaseAction();
        }
    }
}
