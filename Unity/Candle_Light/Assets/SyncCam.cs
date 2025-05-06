using UnityEngine;

public class SyncCam : NetworkBehaviour
{
    [SyncVar] Transform cam1;
    [SyncVar] Transform cam2;

    public void Start(){
        //cam1 = GetComponent<>
    }

    [Client]
    public Update(){
        if(isClient && NetworkClient.active){
            if(StaticVar.characterNumber == 0){
                cam1 = GameManager.GetTransformChangeP1();
            }else{
                cam2 = GameManager.GetTransformChangeP2();
            }
        }
    }
}
