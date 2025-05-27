using UnityEngine;
using Mirror;
using Mirror.SimpleWeb;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Ola, conectei ");
        //GameManager.Instance.startPos = NetworkManager.startPositions;
        foreach (Transform transform in NetworkManager.startPositions)
        {
            GameManager.Instance.startPos.AddRange(transform);
        }
        GameManager.Instance.CheckCharactersDisponibility();
        
    }
    public override void OnClientConnect()
    {
       // startPositions = 
        base.OnClientConnect();
    }
    public override void OnClientDisconnect()
    {

        Debug.Log("Desonectei");
        GameManager.Instance.CheckCharactersDisponibility();
        base.OnClientDisconnect();
    }
    
}
