/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class UseItemInteractable : MonoBehaviour, IInteractable
{

//-------------------------- Variaveis Globais Visiveis --------------------------------

  [Tooltip("Referência para os objetos que receberão os comandos da interação")]
	[SerializeField] 
  private ObserverEventChannel _observerEvent = default;    
  
  private bool action = false;
    
  public ObserverEventChannel GetObserver(){
    return _observerEvent;
  }
  public void SetObserver(ObserverEventChannel observerEvent){
    _observerEvent = observerEvent;
  }

  public void BaseAction(){
    action = !action;
    _observerEvent.NotifyObservers(action? 1:0);
  }
}
