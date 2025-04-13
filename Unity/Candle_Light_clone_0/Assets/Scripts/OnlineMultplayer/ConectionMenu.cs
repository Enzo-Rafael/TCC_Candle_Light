using Mirror;
using UnityEngine;
public class ConectionMenu : NetworkBehaviour
{
    [SerializeField] private NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    public void HostLobby()
    {
       // networkManager = GameObject.FindAnyObjectByType<>
        networkManager.StartHost();
        landingPagePanel.SetActive(false);
    }
}
