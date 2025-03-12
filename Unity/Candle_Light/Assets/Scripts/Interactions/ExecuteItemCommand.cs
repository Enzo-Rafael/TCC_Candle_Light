/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação.

    Bloody Gears - Jogos Digitais SG –  09/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------
using System;
using UnityEngine;

public class ExecuteItemCommand : MonoBehaviour, IObserver
{

    [SerializeField] private ObserverEventChannelSO _observerEvent = default;

    private void OnEnable(){
        _observerEvent.RegisterObserver(this);
    }
    private void OnDisable(){
        _observerEvent.UnregisterObserver(this);
    }
    public void OnEventRaised(ItemSO itemCommand){
        switch(itemCommand.itemType.actionType){
            case ItemTypeSO.ItemActionType.Toggle:
            Debug.Log("ToggleItem");
            break;
            case ItemTypeSO.ItemActionType.Cosume:
            break;
            case ItemTypeSO.ItemActionType.Trigger:
            Debug.Log("Trigger Item");
            break;
        }
    }
}
