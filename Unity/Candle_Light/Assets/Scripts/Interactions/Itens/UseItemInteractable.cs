/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class UseItemInteractable : Interactable, IInteractable
{

  //------------------------- Variaveis Globais privadas -------------------------------
  
  private bool action = false;

  public void BaseAction(){
    action = !action;
    if(_observerEvent != null) _observerEvent.NotifyObservers(action? 1:0);
    ExecuteOrder(action? 1:0);
  }
}
