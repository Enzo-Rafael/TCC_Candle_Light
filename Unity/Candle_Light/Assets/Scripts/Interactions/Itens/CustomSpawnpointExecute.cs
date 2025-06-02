using UnityEngine;

[RequireComponent(typeof(UseSpawnpointInteractable))]
public class CustomSpawnpointExecute : MonoBehaviour, ICodeCustom
{
    UseSpawnpointInteractable spawnReference;
    public void Start(){
        spawnReference = GetComponent<UseSpawnpointInteractable>();
    }
    public void CustomBaseAction(object additionalInformation)
    {
        if (spawnReference != null) spawnReference.SetAction(false);

    }

    public void SetSpawnReference(){
        spawnReference.SetAction(true);
    }
}
