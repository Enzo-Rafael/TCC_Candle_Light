using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Actuator Event Channel")]
public class ActuatorEventChannelSO : ScriptableObject
{
    public UnityAction<bool, ExecuteItemCommand> OnEventRaised;
    
    public void RaiseEvent(bool action, ExecuteItemCommand atuactor){
        if(OnEventRaised != null){
            OnEventRaised.Invoke(action, atuactor);
        }
    } 
}
