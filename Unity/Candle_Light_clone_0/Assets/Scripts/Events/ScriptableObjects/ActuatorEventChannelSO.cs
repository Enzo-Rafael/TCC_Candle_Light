using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Actuator Event Channel")]
public class ActuatorEventChannelSO : ScriptableObject
{
    public UnityAction<int, ExecuteItemCommand> OnEventRaised;
    
    public void RaiseEvent(int action, ExecuteItemCommand atuactor){
        if(OnEventRaised != null){
            OnEventRaised.Invoke(action, atuactor);
        }
    } 
}
