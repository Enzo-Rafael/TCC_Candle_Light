/**************************************************************
    Jogos Digitais SG
    ItemInteractable

    Descrição: Gerencia as funções do item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class ItemInteractable : MonoBehaviour
{

//-------------------------- Variaveis Globais Visiveis --------------------------------

  [Tooltip("Referência para as informações basicas do item")]
	[SerializeField] 
  private ItemSO _item = default;

  [Tooltip("Referência para os objetos que receberão os comandos da interação")]
	[SerializeField] 
  private ObserverEventChannelSO _observerEvent = default;    

  private bool action = false;
    
  public ItemSO GetItem(){
		return _item;
	}

  public ObserverEventChannelSO GetObserver(){
    return _observerEvent;
  }

  public void SetObserver(ObserverEventChannelSO observerEvent){
    _observerEvent = observerEvent;
  }

	public void SetItem(ItemSO item){
		_item = item;
	}

  public void BaseAction(){
    action = !action;
    _observerEvent.NotifyObservers(action);
  }
}
