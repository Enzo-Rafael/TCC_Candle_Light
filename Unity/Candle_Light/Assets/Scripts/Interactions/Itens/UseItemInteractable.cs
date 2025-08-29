/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEditor.VersionControl;
using UnityEngine;

public class UseItemInteractable : Interactable, IInteractable
{

  //------------------------- Variaveis Globais privadas -------------------------------
  
  private bool action = false;
  private int message = 0;
  public void BaseAction(){
    action = !action;
    message = action ? 1 : 0;
    if (_observerEvent != null){
      foreach (var channel in _observerEvent){
        if (channel != null){
          channel.NotifyObservers(message);
        }
      }
    }
    ExecuteOrder(message);
  }
}
