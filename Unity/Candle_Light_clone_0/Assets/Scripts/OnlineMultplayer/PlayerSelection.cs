using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public GameObject Player;

    public GameObject[] selectedPlayerPrefab;

    public void SelectedPlayer(int indice){

        Player = selectedPlayerPrefab[indice];
        Debug.Log(selectedPlayerPrefab[indice]);

    }
}
