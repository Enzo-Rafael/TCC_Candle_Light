using UnityEngine;
using Mirror;
using Mirror.SimpleWeb;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Ola, conectei ");
    }
    
}
