using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SyncVar]
    public List<Transform> spawnLocations = new List<Transform>();//Lista dos Locais onde os personagens devem spawnar
    

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
