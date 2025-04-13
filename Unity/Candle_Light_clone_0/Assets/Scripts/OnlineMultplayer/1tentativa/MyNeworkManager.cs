using UnityEngine;
using Mirror;
public class MyNetworkManager : NetworkManager
{
    public bool SpawnAsCharacter = true;
        public static new MyNetworkManager singleton => (MyNetworkManager)NetworkManager.singleton;
        private PlayerData playerData;

        public override void Awake()
        {
            playerData = PlayerData.playerDataSingleton;
            if (playerData == null)
            {
                Debug.Log("Add CharacterData prefab singleton into the scene.");
                return;
            }
            base.Awake();
        }

        public struct CreateCharacterMessage : NetworkMessage
        {
            public string playerName;
            public int characterNumber;
        }

        public struct ReplaceCharacterMessage : NetworkMessage
        {
            public CreateCharacterMessage createCharacterMessage;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
            NetworkServer.RegisterHandler<ReplaceCharacterMessage>(OnReplaceCharacterMessage);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            if (SpawnAsCharacter)
            {
                // you can send the message here, or wherever else you want
                CreateCharacterMessage characterMessage = new CreateCharacterMessage
                {
                    playerName = StaticVar.playerName,
                    characterNumber = StaticVar.characterNumber
                };

                NetworkClient.Send(characterMessage);
            }
        }

        void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
        {
            Transform startPos = GetStartPosition();

            // check if the save data has been pre-set
            if (message.playerName == "")
            {
                Debug.Log("OnCreateCharacter name invalid or not set, use random.");
                message.playerName = "Player: " + UnityEngine.Random.Range(100, 1000);
            }

            // check that prefab is set, or exists for saved character number data
            // could be a cheater, or coding error, or different version conflict
            if (message.characterNumber <= 0 || message.characterNumber >= playerData.playerPrefabs.Length)
            {
                Debug.Log("OnCreateCharacter prefab Invalid or not set, use random.");
                message.characterNumber = UnityEngine.Random.Range(1, playerData.playerPrefabs.Length);
            }

            // check if the save data has been pre-set
            /*if (message.characterColour == new Color(0, 0, 0, 0))
            {
                Debug.Log("OnCreateCharacter colour invalid or not set, use random.");
                message.characterColour = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            }*/

            GameObject playerObject = startPos != null
                ? Instantiate(playerData.playerPrefabs[message.characterNumber], startPos.position, startPos.rotation)
                : Instantiate(playerData.playerPrefabs[message.characterNumber]);


            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            PlayerSelection characterSelection = playerObject.GetComponent<PlayerSelection>();
            characterSelection.playerName = message.playerName;
            characterSelection.characterNumber = message.characterNumber;

            // call this to use this gameobject as the primary controller
            NetworkServer.AddPlayerForConnection(conn, playerObject);
        }

        void OnReplaceCharacterMessage(NetworkConnectionToClient conn, ReplaceCharacterMessage message)
        {
            // Cache a reference to the current player object
            GameObject oldPlayer = conn.identity.gameObject;

            GameObject playerObject = Instantiate(playerData.playerPrefabs[message.createCharacterMessage.characterNumber], oldPlayer.transform.position, oldPlayer.transform.rotation);

            // Instantiate the new player object and broadcast to clients
            NetworkServer.ReplacePlayerForConnection(conn, playerObject, ReplacePlayerOptions.KeepActive);

            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            PlayerSelection characterSelection = playerObject.GetComponent<PlayerSelection>();
            characterSelection.playerName = message.createCharacterMessage.playerName;
            characterSelection.characterNumber = message.createCharacterMessage.characterNumber;

            // Remove the previous player object that's now been replaced
            // Delay is required to allow replacement to complete.
            Destroy(oldPlayer, 0.1f);
        }

        public void ReplaceCharacter(ReplaceCharacterMessage message)
        {
            NetworkClient.Send(message);
        }
    }

/*public class MyNetworkManager : NetworkManager
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
    public ReplacePlayerOptions replacePlayerOptions;
    public NetworkConnectionToClient client;
    public GameObject p2;

    public void ReplacePlayer(NetworkConnectionToClient conn, GameObject newPrefab)
{
    
    GameObject oldPlayer = conn.identity.gameObject;

  
    NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab),  replacePlayerOptions);

   
    Destroy(oldPlayer, 0.1f);
}
}*/