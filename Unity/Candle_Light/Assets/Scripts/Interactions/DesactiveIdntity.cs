using UnityEngine;
using Mirror;

public class DesactiveIdntity : NetworkBehaviour
{
    [SerializeField]
    private NetworkIdentity identity;
    private GameObject father;
    // Update is called once per frame
    [Server]
    void Update()
    {
        father = transform.parent.gameObject;
        if(father == null){
            identity.enabled = true;
            return;    
        }else{
            identity.enabled = false;
        }
    }
}
