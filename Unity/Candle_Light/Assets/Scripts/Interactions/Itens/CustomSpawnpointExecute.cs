using UnityEngine;

[RequireComponent(typeof(UseSpawnpointInteractable))]
public class CustomSpawnpointExecute : MonoBehaviour, ICustomCode
{
    public void CustomBaseAction(object additionalInformation){
        UseSpawnpointInteractable spawnReference = (UseSpawnpointInteractable) additionalInformation;
        Debug.Log("Executei");
        spawnReference.SetAction(false);
    }  
    
}
