using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public PlayerSelection PlayerSelection;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        GameObject playerPrefab = PlayerSelection.Player;
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null

            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}