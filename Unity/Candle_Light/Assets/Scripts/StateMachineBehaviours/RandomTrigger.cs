using UnityEngine;

/// <summary>
/// Ativa o trigger com o nome definido daqui a uma quantidade variavel de segundos.
/// </summary>
public class RandomTrigger : StateMachineBehaviour
{
    [Tooltip("Tempo minimo ate o trigger")]
    [SerializeField] private float minTime;

    [Tooltip("Tempo maximo ate o trigger")]
    [SerializeField] private float maxTime;
    
    [Tooltip("Nome do trigger")]
    [SerializeField] private string triggerName;
    
    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            animator.SetTrigger(triggerName);
            timer = Random.Range(minTime, maxTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
