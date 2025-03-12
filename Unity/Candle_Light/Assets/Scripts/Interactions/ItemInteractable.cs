/**************************************************************
    Jogos Digitais SG
    Iteminteractable

    Descrição: Gerencia as funções do item.

    Bloody Gears - Jogos Digitais SG –  06/03/2024
    Modificado por: Italo 
    Referencias: Unity ChopyChopy
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------
using UnityEngine;
using System.Collections.Generic;

public class ItemInteractable : MonoBehaviour
{

//-------------------------- Variaveis Globais Visiveis --------------------------------

	[SerializeField] private ItemSO item = default;
	[SerializeField] private ObserverEventChannelSO _observerEvent = default;    
    
  void Start(){
        
  }
  void Update(){
    if(Input.GetKeyDown(KeyCode.Space)){
      _observerEvent.NotifyObservers(item);
    }
  }
}
