/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class UseMultipleItemInteraction : Interactable, IInteractable
{

//-------------------------- Variaveis Globais Visiveis -------------------------------- 
  
  [Tooltip("Qual a Ordem ele deve ser Ativado")]
  [SerializeField] 
  private int orderID;

  //------------------------- Variaveis Globais privadas -------------------------------

  private bool action = false;
    
  /*------------------------------------------------------------------------------
  Função:     BaseAction
  Descrição:  Ação executada ao interagir com o item
  Entrada:    -
  Saída:      -
  ------------------------------------------------------------------------------*/
  public void BaseAction(){
    action = !action;
    if(_observerEvent != null)_observerEvent.NotifyObservers(action? 1:0, orderID);
    ExecuteOrder(action? 1:0);
  }
}

