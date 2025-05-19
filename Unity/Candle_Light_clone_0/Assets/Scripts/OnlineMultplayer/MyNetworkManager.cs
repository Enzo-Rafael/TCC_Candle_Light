using UnityEngine;
using Mirror;
using Mirror.SimpleWeb;
using UnityEditor.PackageManager;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Ola, conectei ");
        
    }
    public override void OnClientDisconnect()
    {

        Debug.Log("Desonectei ");

        base.OnClientDisconnect();
    }
    
}
