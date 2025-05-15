using UnityEngine;

[RequireComponent(typeof(UseSpawnpointInteractable))]
public class CustomSpawnpointExecute : MonoBehaviour, ICodeCustom
{
    public void CustomBaseAction(object additionalInformation){
        UseSpawnpointInteractable spawnReference = (UseSpawnpointInteractable) additionalInformation;
        Debug.Log("Executei");
        spawnReference.SetAction(false);
    }
}
