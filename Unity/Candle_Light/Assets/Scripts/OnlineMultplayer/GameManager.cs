using Mirror;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    //Variavei para os indices e posicoes pra seicronização
    [SyncVar]
    public int charIndex;
    [SyncVar]
    public int camNewPosP1;
    [SyncVar]
    public Transform camNewPosP2;
    [SyncVar]
    public bool player01 = false;
    [SyncVar]
    public bool player02 = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        // NetworkManager.startPositions = spawnLocations;
    }
    [Server]
    public void CheckCharactersDisponibility()
    {
        if (GameObject.FindGameObjectWithTag("Player1") == false)
        {
            player01 = false;
        }
        if (GameObject.FindGameObjectWithTag("Player2") == false)
        {
            player02 = false;
        }
    }

    [Server]
    public void TransformChangeP2(Transform transformCam)
    {
       camNewPosP2 = transformCam;
    }
    [Server]
    public Transform SetPos()
    {
        return camNewPosP2;
    }
    [Server]
    public Transform SetSpawn()
    {
        return NetworkManager.startPositions[charIndex];
    }
    [Server]
    public void SetIndexCurrent(int index)
    {
        charIndex = index;
    }
    
}
