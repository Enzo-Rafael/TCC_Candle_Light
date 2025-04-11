using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData playerDataSingleton {get; private set;}
    public GameObject[] playerPrefabs;
    public void Awake()
        {
            playerDataSingleton = this;
        }
}
