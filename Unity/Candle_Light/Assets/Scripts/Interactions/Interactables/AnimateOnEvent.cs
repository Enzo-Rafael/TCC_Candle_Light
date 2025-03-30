using UnityEngine;

public class AnimateOnEvent : MonoBehaviour, IObserver
{
    [Tooltip("Referência para o evento sendo escutado.")]
	[SerializeField] 
    private ObserverEventChannel _observerEvent = default;

    [Tooltip("Referência para o controlador de animacao.")]
	[SerializeField] 
    private Animator animator;

    [Tooltip("Tipo do parametro de animador a ser modificado.")]
    [SerializeField]
    private AnimatorControllerParameterType parameterType = AnimatorControllerParameterType.Trigger;

    [Tooltip("Nome do parametro de animador a ser modificado.")]
    [SerializeField]
    private string parameterName;

    private void Awake()
    {
        _observerEvent.RegisterObserver(this);
    }

    public void OnEventRaised(int message)
    {
        switch(parameterType)
        {
            case AnimatorControllerParameterType.Trigger:
                animator.SetTrigger(parameterName);
            break;

            case AnimatorControllerParameterType.Bool:
                animator.SetBool(parameterName, message != 0);
            break;

        }
    }

}
