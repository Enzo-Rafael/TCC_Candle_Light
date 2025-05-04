using UnityEngine;

/// <summary>
/// Ativa um parametro de animacao ao sair do estado. Tipos de parametro aceitos: trigger e bool
/// </summary>
public class SetParameterOnExit : StateMachineBehaviour
{
    [Tooltip("Parametros implementados: bool e trigger")]
    [SerializeField]
    private AnimatorControllerParameterType parameterType;

    [SerializeField]
    private string parameterName;

    [SerializeField]
    private bool value;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      switch(parameterType)
      {
          case AnimatorControllerParameterType.Trigger:
              if(value)
                  animator.SetTrigger(parameterName);
              else
                  animator.ResetTrigger(parameterName);
          break;

          case AnimatorControllerParameterType.Bool:
              animator.SetBool(parameterName, value);
          break;
      } 
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
