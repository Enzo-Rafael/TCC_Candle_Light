using UnityEngine;

public class UIManager : MonoBehaviour
{
     public PlayerSelection playerSelection;
    public void OnSelectPlayer1(){
        playerSelection.SelectPlayer(playerPrefab1);
    }  
    public void OnSelectPlayer2(){
        playerSelection.SelectPlayer(playerPrefab2);
    }
}
