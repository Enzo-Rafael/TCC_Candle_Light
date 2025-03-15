/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação com determinado item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System;
using Unity.VisualScripting;
using UnityEngine;

public class ExecuteItemCommand : MonoBehaviour, IObserver
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Header("Transmitindo em:")] 
    [Tooltip("Referência para se inscrever na lista de Observers de determinado item")]
    [SerializeField] 
    private ObserverEventChannelSO _observerEvent = default;

    //-------------------------- Variaveis Globais Privadas --------------------------------

    private bool bolean = false;

    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Registra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _observerEvent.RegisterObserver(this);
    }

    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _observerEvent.UnregisterObserver(this);
    }

    /*------------------------------------------------------------------------------
    Função:     OnEventRaised
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    ItemSO -  Utlizado para saber o tipo de interação que o item quer que seja feita.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnEventRaised(ItemSO itemCommand){
        switch(itemCommand.itemType.actionType){
            case ItemTypeSO.ItemActionType.Toggle:
            Debug.Log("Toggle Item");
            ToggleItem();
            break;
            case ItemTypeSO.ItemActionType.Cosume:
            Debug.Log("Consume Item");
            break;
            case ItemTypeSO.ItemActionType.Trigger:
            Debug.Log("Trigger Item");
            break;
        }
    }

    private void ToggleItem(){
        bolean = !bolean;
        this.gameObject.SetActive(bolean);
    }
}
