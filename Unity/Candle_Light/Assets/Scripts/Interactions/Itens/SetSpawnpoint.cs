using UnityEngine;

[RequireComponent(typeof(CustomSpawnpointExecute))]
public class SetSpawnpoint : MonoBehaviour
{
    CustomSpawnpointExecute _customSpawnpointExecute;
    void Start(){
        _customSpawnpointExecute = GetComponent<CustomSpawnpointExecute>();
        _customSpawnpointExecute.SetSpawnReference();
    }
}
