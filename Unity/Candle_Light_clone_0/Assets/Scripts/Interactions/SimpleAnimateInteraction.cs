using UnityEngine;

public class SimpleAnimateInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string paramName;
    public void BaseAction()
    {
        animator.SetTrigger(paramName);
    }
}
