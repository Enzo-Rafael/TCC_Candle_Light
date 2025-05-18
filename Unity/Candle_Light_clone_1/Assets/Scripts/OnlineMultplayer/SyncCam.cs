using UnityEngine;
using Mirror;
public class SyncCam : NetworkBehaviour
{
    [SyncVar] Transform cam1;
    [SyncVar] Transform cam2;
    public int x;
    public void Start(){
        //cam1 = GetComponent<>
    }

    [Client]
    public void Update(){
        if(isClient && NetworkClient.active){
            if( x == 0){
               // cam1 = GameManager.Instance.GetTransformChangeP1();
            }else{
               // cam2 = GameManager.Instance.GetTransformChangeP2();
            }
        }
    }
}
