using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public static GameObject Player;

    public GameObject[] selectedPlayerPrefab;

    public void SelectedPlayer(int indice){

        Player = selectedPlayerPrefab[indice];
        Debug.Log(selectedPlayerPrefab[indice]);

    }
}
