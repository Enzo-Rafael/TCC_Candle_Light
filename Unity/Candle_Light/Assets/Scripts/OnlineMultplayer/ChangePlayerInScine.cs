using UnityEngine;
using Mirror;
public class ChangePlayerInScine : MonoBehaviour
{
    public ReplacePlayerOptions replacePlayerOptions;
    public NetworkConnectionToClient client;
    public GameObject p2;


    public void ReplacePlayer(NetworkConnectionToClient conn, GameObject newPrefab)
{
    
    GameObject oldPlayer = conn.identity.gameObject;

  
    NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab),  replacePlayerOptions);

   
    Destroy(oldPlayer, 0.1f);
}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            //client = MyNetworkManager.singleton.
            ReplacePlayer(client, p2.GetComponent<MyNetworkManager>().spawnPrefabs[1]);
        }
    }
}
