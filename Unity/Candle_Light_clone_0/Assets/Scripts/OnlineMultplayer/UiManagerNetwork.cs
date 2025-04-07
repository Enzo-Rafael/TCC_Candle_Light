using System;
using UnityEngine;

public class UiManagerNetwork : MonoBehaviour
{
    public PlayerSelection playerSelection;

    public void OnSelectPlayer1(){
        playerSelection.SelectedPlayer(0);
    }
    public void OnSelectPlayer2(){
        playerSelection.SelectedPlayer(1);
    }
}
