using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player1Muv GetIput;

    private void Awake()
    {
       GetIput = GetComponent<Player1Muv>();
    }
}
