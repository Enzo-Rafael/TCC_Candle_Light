using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SyncVar]
    public Transform[] spaunLocations = default;//Lista dos Locais onde os personagens devem spawnar
    //public Transform transformCamP1;
    //public Transform transformCamP2;

    [SyncVar]
    public float matchTime = 0f;

    private void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /*[Server]
    public void TransformChangeP1(Transform cam){
        transformCamP1 = cam;
    }
    [Server]
    public void TransformChangeP2(Transform cam){
        transformCamP2 = cam;
    }
    [Server]
    public Transform GetTransformChangeP1(){
        return transformCamP1;
    }
    [Server]
    public Transform GetTransformChangeP2(){
        return transformCamP2;
    }*/
}
