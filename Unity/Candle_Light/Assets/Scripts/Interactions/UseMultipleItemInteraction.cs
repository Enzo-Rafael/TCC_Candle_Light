/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class UseMultipleItemInteraction : MonoBehaviour, IInteractable
{

//-------------------------- Variaveis Globais Visiveis --------------------------------

  [Tooltip("Referência para os objetos que receberão os comandos da interação")]
  [SerializeField] 
  private ObserverEventChannel _observerEvent = default;    
  
  [Tooltip("Qual a Ordem ele deve ser Ativado")]
  [SerializeField] 
  private int orderID;

  private bool action = false;
    
  public ObserverEventChannel GetObserver(){
    return _observerEvent;
  }
  public void SetObserver(ObserverEventChannel observerEvent){
    _observerEvent = observerEvent;
  }

  public void BaseAction(){
    action = !action;
    _observerEvent.NotifyObservers(action? 1:0, orderID);
    Debug.Log("Uepa");
  }
}

